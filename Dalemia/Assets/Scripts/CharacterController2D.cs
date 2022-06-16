using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    KeyCode JumpKey;

    [SerializeField]
    KeyCode LeftKey;

    [SerializeField]
    KeyCode RightKey;

    public LayerMask GroundLayer;

    private bool IsGrounded;
    public bool IsWalking;
    private bool IsFlipped;

    private float MovementSpeed = 2.8f;
    private float JumpForce = 5.0f;

    void Update()
    {
        IsGrounded = CollidesWithGround(this.transform);
        IsWalking = GetMovement() != Mathf.Floor(0);

        Vector2 Movement = new Vector2(GetMovement(), 0f);

        transform.position += (Vector3)Movement * Time.deltaTime * MovementSpeed;

        if (Input.GetKeyDown(JumpKey))
            Jump();
        
        if (Input.GetKeyDown(LeftKey) && !IsFlipped)
            Flip();
        if (Input.GetKeyDown(RightKey) && IsFlipped)
            Flip();
    }

    void Jump()
    {
        if (IsGrounded)
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
    }

    void Flip()
    {
        IsFlipped = !IsFlipped;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    int GetMovement()
    {
        int result = 0;
        if (Input.GetKey(LeftKey))
            result += -1;
        if (Input.GetKey(RightKey))
            result += 1;

        return result;
    }

    bool CollidesWithGround(Transform Object)
    {
        Vector2 GroundCheck = new Vector2( Object.position.x, Object.position.y - 2.0f );
        return Physics2D.OverlapCircle(GroundCheck, 0.2f, GroundLayer);
    }
}