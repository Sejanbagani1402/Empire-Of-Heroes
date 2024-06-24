using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to the Animator component
    private Animator animator;

    // Movement variables
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true; // To keep track of character facing direction

    // Parameters for animator
    private int isRunningHash;
    private int groundedHash;
    private int attackHash;
    private int jumpHash;
    private int hurtHash;
    private int deathHash;

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Initialize parameter hashes
        isRunningHash = Animator.StringToHash("isRunning");
        groundedHash = Animator.StringToHash("Grounded");
        attackHash = Animator.StringToHash("Attack");
        jumpHash = Animator.StringToHash("Jump");
        hurtHash = Animator.StringToHash("Hurt");
        deathHash = Animator.StringToHash("Death");

        // Freeze rotation to prevent character from rotating when jumping
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

        // Update animator
        animator.SetBool(isRunningHash, moveInput != 0);

        // Flip character direction based on movement input
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse); // Adjust jump force as needed
            animator.SetTrigger(jumpHash);
        }

        // Update grounded state
        animator.SetBool(groundedHash, isGrounded);
    }

    void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1")) // Assuming Fire1 is your attack button
        {
            animator.SetTrigger(attackHash);
        }
    }

    void Flip()
    {
        // Switch the way the player is facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
