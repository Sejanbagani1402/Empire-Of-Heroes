using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving;
    private Vector3 currentTargetPos;
    private Coroutine moveCoroutine;

    public LayerMask solidObjectLayer;
    public LayerMask npcLayer;

    // Reference to the background GameObject with a Collider2D
    public GameObject background;

    private Collider2D backgroundCollider;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        backgroundCollider = background.GetComponent<Collider2D>();
    }

    private void Start()
    {
        // Set initial direction to face up when entering a new area
        SetInitialDirection(Vector2.up);
    }

    private void Update()
    {
        // Check if the pointer is over a UI element before processing movement
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            HandlePlayerMovementInput();
        }
    }

    private void HandlePlayerMovementInput()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 targetPos = Vector3.zero;

            // Handle mouse click
            if (Input.GetMouseButtonDown(0))
            {
                targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            // Handle touch
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                targetPos = Camera.main.ScreenToWorldPoint(touch.position);
            }

            targetPos.z = 0; // Assuming 2D movement on the XY plane

            // Stop the current movement coroutine if it is running
            if (isMoving && moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                isMoving = false;
                if (animator != null)
                {
                    animator.SetBool("isMoving", false);
                }
            }

            // Clamp target position within the bounds of the background collider
            currentTargetPos = ClampPositionWithinBackground(targetPos);

            // Start a new movement coroutine
            moveCoroutine = StartCoroutine(Move(currentTargetPos));
        }
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        if (animator != null)
        {
            animator.SetBool("isMoving", true);
        }

        while (isMoving)
        {
            // Move towards the target position step by step
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            // Check if the next position is walkable
            if (!IsWalkable(nextPosition))
            {
                isMoving = false;
                if (animator != null)
                {
                    animator.SetBool("isMoving", false);
                }
                yield break;
            }

            // Clamp next position within the bounds of the background collider
            nextPosition = ClampPositionWithinBackground(nextPosition);

            // Update animation parameters based on current movement direction
            Vector3 direction = nextPosition - transform.position;
            if (animator != null)
            {
                // Normalize the direction to get the movement direction (either -1, 0, or 1)
                direction.Normalize();
                animator.SetFloat("moveX", direction.x);
                animator.SetFloat("moveY", direction.y);
            }

            // Move the player
            transform.position = nextPosition;

            // Check if the player has reached the target position
            if ((targetPos - transform.position).sqrMagnitude <= Mathf.Epsilon)
            {
                isMoving = false;
                if (animator != null)
                {
                    animator.SetBool("isMoving", false);
                }
            }

            yield return null;
        }
    }

    private Vector3 ClampPositionWithinBackground(Vector3 position)
    {
        if (backgroundCollider != null)
        {
            Bounds bounds = backgroundCollider.bounds;
            position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
            position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
        }
        return position;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        float checkRadius = 0.1f;
        Collider2D hit = Physics2D.OverlapCircle(targetPos, checkRadius, solidObjectLayer | npcLayer);
        return hit == null;
    }

    // Public method to set the initial direction
    public void SetInitialDirection(Vector2 direction)
    {
        if (animator != null)
        {
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
            animator.SetBool("isMoving", false);
        }
    }
}

