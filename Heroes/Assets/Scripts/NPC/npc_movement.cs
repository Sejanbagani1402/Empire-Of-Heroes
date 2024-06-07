using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float movespeed;
    private bool ismoving;

    public LayerMask solidObjectLayer;

    private Vector2 input;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && !ismoving)
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

            if (!ismoving && IsWalkable(targetpos))
            {
                StartCoroutine(Move(targetpos));
            }
        }
    }

    IEnumerator Move(Vector3 initialTargetPos)
    {
        ismoving = true;
        Vector3 currentTargetPos = initialTargetPos;

        if (animator != null)
        {
            animator.SetBool("isMoving", true);
        }

        while (ismoving)
        {
            // Update target position if a new tap occurs
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                // Handle mouse click
                if (Input.GetMouseButtonDown(0))
                {
                    currentTargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                // Handle touch
                else if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    currentTargetPos = Camera.main.ScreenToWorldPoint(touch.position);
                }

                currentTargetPos.z = 0; // Assuming 2D movement on the XY plane
            }

            // Check if the next position is walkable before moving
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, currentTargetPos, movespeed * Time.deltaTime);
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
            Vector3 direction = currentTargetPos - transform.position;
            if (animator != null)
            {
                animator.SetFloat("moveX", direction.x);
                animator.SetFloat("moveY", direction.y);
            }

            transform.position = nextPosition;

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos - transform.position, Vector2.Distance(transform.position, targetPos), solidObjectLayer);
        if (hit.collider != null)
        {
            return false;
        }
        return true;
    }
}
