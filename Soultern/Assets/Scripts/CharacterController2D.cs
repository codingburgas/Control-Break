using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public KeyCode SprintKey;
    public KeyCode JumpKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    public LayerMask GroundLayer;

    [HideInInspector] public bool IsGrounded;
    [HideInInspector] public bool IsWalking;
    [HideInInspector] public bool IsFlipped;
    [HideInInspector] public bool CanUse;
    [HideInInspector] public bool CanJump;
    [HideInInspector] public bool TakeDamage = false;

    private float MovementSpeed;
    private float WalkSpeed = 2.8f;
    private float SprintSpeed = 6.0f;
    private float JumpForce = 6.0f;

    [HideInInspector] public Vector2 Movement;

    [HideInInspector] public Vector3 Checkpoint;

    private StatsController StatsController;
    private CheckpointManager CheckpointManager;

    void Start()
    {
        StatsController = GameObject.Find("StatsController").GetComponent<StatsController>();
        CheckpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();

        Time.timeScale = 1f;
        transform.position = CheckpointManager.LatestCheckpoint;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 2.25f, Camera.main.transform.position.z);
    }

    void Update()
    {
        if (StatsController.Health == 0 || Time.timeScale == 0f) return;

        IsGrounded = CollidesWithGround(this.transform);
        IsWalking = GetMovement() != Mathf.Floor(0);

        Movement = new Vector2(GetMovement(), 0f);

        transform.position += (Vector3)Movement * Time.deltaTime * MovementSpeed;

        if (Input.GetKey(SprintKey) && CanUse)
            Sprint();
        else
            Walk();
        
        if (Input.GetKeyDown(JumpKey) && IsGrounded && CanUse)
            Jump();

        if (Input.GetKeyDown(LeftKey) && !IsFlipped)
            Flip();
        if (Input.GetKeyDown(RightKey) && IsFlipped)
            Flip();
    }

    void Walk()
    {
        MovementSpeed = WalkSpeed;
    }

    void Sprint()
    {
        MovementSpeed = SprintSpeed;
    }

    void Jump()
    {
        Launch(0f, JumpForce);
    }

    public void Flip()
    {
        IsFlipped = !IsFlipped;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public int GetMovement()
    {
        int result = 0;
        if (Input.GetKey(LeftKey))
            result += -1;
        if (Input.GetKey(RightKey))
            result += 1;

        return result;
    }

    public Vector3 GetVelocity()
    {
        return this.GetComponent<Rigidbody2D>().velocity;
    }

    bool CollidesWithGround(Transform Object)
    {
        Vector2 GroundCheck = new Vector2( Object.position.x, Object.position.y - 2.0f );
        return Physics2D.OverlapCircle(GroundCheck, 0.2f, GroundLayer);
    }

    void Launch(float ForceX, float ForceY)
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(ForceX, ForceY), ForceMode2D.Impulse);
    }

    void EnableDamage()
    {
        TakeDamage = false;
    }

    IEnumerator DestroyObject(GameObject Object)
    {
        yield return new WaitForSeconds(2f);
        Destroy(Object);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (TakeDamage) return;

        if (other.CompareTag("Checkpoint"))
        {
            Vector3 CheckpointPosition = other.transform.position;

            CheckpointManager.LatestCheckpoint = new Vector3(CheckpointPosition.x, CheckpointPosition.y - 1.5f, CheckpointPosition.z);
        }
        
        if (other.CompareTag("Damage") && !TakeDamage)
        {
            if (other.GetComponent<PumpkinController>().IsDead == true) return;

            StatsController.Health--;
            if (transform.position.x > other.transform.position.x)
                Launch(3.5f, 2f);
            else
                Launch(-3.5f, 2f);

            TakeDamage = true;
        }

        if (other.CompareTag("Destroy") && !TakeDamage)
        {
            GameObject Pumpkin = other.transform.parent.gameObject;

            Pumpkin.GetComponent<PumpkinController>().PumpkinDeath.Play();
            Pumpkin.GetComponent<PumpkinController>().IsDead = true;
            StartCoroutine(DestroyObject(Pumpkin));
            Launch(3f, 10f);
        }

        if (other.CompareTag("Health") && StatsController.Health != StatsController.HealthMax)
        {
            StatsController.Health++;

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Spikes") && !TakeDamage)
        {
            StatsController.Health = 0;
        }

        Invoke("EnableDamage", 0.825f);
    }
}