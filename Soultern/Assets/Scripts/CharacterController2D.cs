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

    private AudioSource Footstep;

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
    private MenuManager MenuManager;
    private DialogueManager DialogueManager;

    void Awake()
    {
        Invoke("DisableWakingUp", 4f);
        GetComponent<Animator>().SetBool("IsWakingUp", true);
    }

    void Start()
    {
        StatsController = GameObject.Find("StatsController").GetComponent<StatsController>();
        CheckpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        MenuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        DialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        if (CheckpointManager.Lives == CheckpointManager.MaxLives)
            Invoke("PlayStartingDialogue", 6.125f);

        Footstep = GetComponent<AudioSource>();

        Time.timeScale = 1f;
        transform.position = CheckpointManager.LatestCheckpoint;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 2.25f, Camera.main.transform.position.z);
    }

    void Update()
    {
        if (StatsController.Health == 0 || Time.timeScale == 0f || DialogueManager.IsInDialogue || GetComponent<Animator>().GetBool("IsWakingUp"))
        {
            Footstep.Stop();
            return;
        }

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

        if (Input.GetKey(LeftKey) && !IsFlipped)
            Flip();
        if (Input.GetKey(RightKey) && IsFlipped)
            Flip();
        
        if (GetMovement() != 0 && !Footstep.isPlaying && IsGrounded)
            Footstep.Play();
        else if (GetMovement() == 0 || !IsGrounded)
            Footstep.Stop();
    }

    void Walk()
    {
        MovementSpeed = WalkSpeed;
        
        Footstep.pitch = 0.9f;
    }

    void Sprint()
    {
        MovementSpeed = SprintSpeed;

        Footstep.pitch = 1.125f;
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

    void DisableWakingUp()
    {
        GetComponent<Animator>().SetBool("IsWakingUp", false);
    }

    void PlayStartingDialogue()
    {
        DialogueManager.StartDialogue(new Vector2(1, 2));
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
            other.gameObject.transform.Find("Hram").GetComponent<Animator>().SetBool("IsActivated", true);

            CheckpointManager.LatestCheckpoint = new Vector3(CheckpointPosition.x, CheckpointPosition.y - 1.5f, CheckpointPosition.z);
        }
        
        if (other.CompareTag("Damage") && !TakeDamage)
        {
            if (other.GetComponent<PumpkinController>().IsDead == true) return;

            StatsController.Health--;
            other.GetComponent<PumpkinController>().DamageSound.Play();

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
            Launch(3f, 10.75f);
        }

        if (other.CompareTag("Health") && StatsController.Health != StatsController.HealthMax)
        {
            StatsController.Health++;

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Spikes"))
        {
            StatsController.Health = 0;
        }

        if (other.CompareTag("Credits"))
        {
            MenuManager.Credits();
        }

        Invoke("EnableDamage", 0.825f);
    }
}