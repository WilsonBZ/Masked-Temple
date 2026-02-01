using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator aPress;
    [SerializeField] private Animator dPress;
    [SerializeField] private Animator sbPress;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    public bool canMove = true;

    // Horizontal Movement
    [SerializeField] private float movementSpeed = 1.0f;
    private float horizontalInput;
    private float scaleX;

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

    private float volume = 0.2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.limeGreen;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }

    private void Start()
    {
        scaleX = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateTimer();

        HorizontalMovement();
        Jump();
        
        UpdateAnimations();
    }

    private void HorizontalMovement()
    {
        if (!canMove)
        {
            horizontalInput = 0;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * movementSpeed, rb.linearVelocity.y);

        if (aPress != null && horizontalInput != 0)
        {
            aPress.GetComponent<FadeAway>().Fade();
            dPress.GetComponent<FadeAway>().Fade();
        }
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
        if (!canMove)
            return;

        if (Input.GetButtonDown("Jump"))
        {
            bufferTimer = bufferTime;
        }

        if (CheckCanJump() && (Input.GetButtonDown("Jump") || bufferTimer > 0) && !isJumping)
        {
            bufferTimer = 0;
            rb.linearVelocityY = 0;
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            isJumping = true;

            audioSource.pitch = Random.Range(0.3f, 0.5f);
            audioSource.volume = volume;
            audioSource.PlayOneShot(jumpSound);

            if (sbPress != null && sbPress.GetComponent<FadeAway>().isActiveAndEnabled)
            {
                sbPress.GetComponent<FadeAway>().Fade();
            }
        }
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isMasked", false);

        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(scaleX * -1, transform.localScale.y, transform.localScale.z);
            animator.SetBool("isRunning", true);
        }

        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
            animator.SetBool("isRunning", true);
        }

        CheckCanJump();

        if (!isGrounded)
        {
            animator.SetBool("isGrounded", false);
        } 

        if (MaskManager.maskNum == 1)
            animator.SetBool("isMasked", true);
    }
}