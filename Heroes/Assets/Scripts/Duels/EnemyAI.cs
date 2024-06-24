using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Reference to the Animator component
    private Animator animator;

    // Movement variables
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true; // To keep track of character facing direction

    // AI variables
    public Transform player; // Reference to the player
    public float attackRange = 1f; // Range within which the enemy will attack
    public float attackCooldown = 1f; // Time between attacks
    private float lastAttackTime;

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
        HandleAttack();
    }

    void HandleMovement()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        //Debug.Log("Distance to player: " + distanceToPlayer);

        if (distanceToPlayer > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 moveVelocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            //Debug.Log("Moving with velocity: " + moveVelocity);
            rb.velocity = moveVelocity;

            // Update animator
            animator.SetBool(isRunningHash, true);

            // Flip character direction based on movement direction
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }
        else
        {
            // Stop moving
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool(isRunningHash, false);
        }
    }

    void HandleAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Debug.Log("Checking attack range. Distance: " + distanceToPlayer + ", Attack range: " + attackRange);

        if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            Debug.Log("Attacking player. Time: " + Time.time);
            // Attack animation
            animator.SetTrigger(attackHash);
            lastAttackTime = Time.time;
        }
    }

    void Flip()
    {
        // Switch the way the enemy is facing.
        facingRight = !facingRight;

        // Multiply the enemy's x local scale by -1.
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
