using System.Collections;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float movespeed = 5f;
    private bool ismoving;
    private Vector3 currentTargetPos;

    public LayerMask solidObjectLayer;
    public LayerMask npcLayer;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 targetpos = Vector3.zero;

            // Handle mouse click
            if (Input.GetMouseButtonDown(0))
            {
                targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            // Handle touch
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                targetpos = Camera.main.ScreenToWorldPoint(touch.position);
            }

            targetpos.z = 0; // Assuming 2D movement on the XY plane

            currentTargetPos = targetpos;

            if (!ismoving)
            {
                StartCoroutine(Move(currentTargetPos));
            }
        }
    }


    IEnumerator Move(Vector3 initialTargetPos)
    {
        ismoving = true;

        if (animator != null)
        {
            animator.SetBool("isMoving", true);
        }

        while (ismoving)
        {
            // Move towards the target position step by step
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, currentTargetPos, movespeed * Time.deltaTime);

            // Check if the next position is walkable
            if (!IsWalkable(nextPosition))
            {
                ismoving = false;
                if (animator != null)
                {
                    animator.SetBool("isMoving", false);
                }
                yield break;
            }

            // Update animation parameters based on current movement direction
            Vector3 direction = nextPosition - transform.position;
            if (animator != null)
            {
                animator.SetFloat("moveX", direction.x);
                animator.SetFloat("moveY", direction.y);
            }

            // Move the player
            transform.position = nextPosition;

            // Check if the player has reached the target position
            if ((currentTargetPos - transform.position).sqrMagnitude <= Mathf.Epsilon)
            {
                ismoving = false;
                if (animator != null)
                {
                    animator.SetBool("isMoving", false);
                }
            }

            yield return null;
        }
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        float checkRadius = 0.1f; // Adjust the radius as needed
        Collider2D hit = Physics2D.OverlapCircle(targetPos, checkRadius, solidObjectLayer | npcLayer);
        return hit == null;
    }
}
