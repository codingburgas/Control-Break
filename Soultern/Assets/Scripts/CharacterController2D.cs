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

    private float MovementSpeed;
    private float WalkSpeed = 2.8f;
    private float SprintSpeed = 6.0f;
    private float JumpForce = 5.0f;

    [HideInInspector] public Vector3 Checkpoint;

    private StatsController StatsController;

    void Start()
    {
        StatsController = GameObject.Find("StatsController").GetComponent<StatsController>();
    }

    void Update()
    {
        IsGrounded = CollidesWithGround(this.transform);
        IsWalking = GetMovement() != Mathf.Floor(0);

        Vector2 Movement = new Vector2(GetMovement(), 0f);

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
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
            Checkpoint = transform.position;
        
        if (other.CompareTag("Damage"))
            StatsController.Health--;
        if (other.CompareTag("Destroy"))
        {
            Destroy(other.transform.parent.gameObject);
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(3f, 7.5f), ForceMode2D.Impulse);
        }

        if (other.CompareTag("Health") && StatsController.Health != StatsController.HealthMax)
        {
            StatsController.Health++;
            Destroy(other.gameObject);
        }
    }
}