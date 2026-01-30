using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    // Horizontal Movement
    [SerializeField] private float movementSpeed = 1.0f;
    private float horizontalInput;

    // Jump
    [SerializeField] private float jumpPower = 1.0f;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    private bool isJumping = false;
    private bool isGrounded = true;

    // Coyote Jump
    private float coyoteTime = 0.2f;
    private float coyoteTimer;

    // Jump Buffer
    private float bufferTime = 0.2f;
    private float bufferTimer;

    private Rigidbody2D rb;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateTimer();
        HorizontalMovement();
        Jump();
    }

    private void HorizontalMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * movementSpeed, rb.linearVelocity.y);
    }

    private void UpdateTimer()
    {
        coyoteTimer = Mathf.Clamp(coyoteTimer - Time.deltaTime, 0, coyoteTime);
        bufferTimer = Mathf.Clamp(bufferTimer - Time.deltaTime, 0, bufferTime);
    }

    private bool CheckCanJump()
    {
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundMask))
        {
            if (!isGrounded)
                isJumping = false;

            isGrounded = true;
            coyoteTimer = coyoteTime;
            return true;
        }

        isGrounded = false;

        if (coyoteTimer > 0.0f)
            return true;

        return false;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            bufferTimer = bufferTime;
        }

        if (CheckCanJump() && (Input.GetButtonDown("Jump") || bufferTimer > 0) && !isJumping)
        {
            rb.linearVelocityY = 0;
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            isJumping = true;
        }
    }
}