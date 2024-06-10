using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCcontroller : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] List<Vector2> path;
    [SerializeField] float waitTimeAtPoint = 1.0f;

    NPCAnimator npcAnimator;
    int currentPathIndex = 0;
    bool isMoving = false;

    Rigidbody2D rb;

    void Start()
    {
        npcAnimator = GetComponent<NPCAnimator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FollowPath());
    }

    void Update()
    {
        if (isMoving)
        {
            Vector2 direction = (path[currentPathIndex] - (Vector2)transform.position).normalized;
            npcAnimator.MoveX = direction.x;
            npcAnimator.MoveY = direction.y;
        }
        else
        {
            npcAnimator.MoveX = 0;
            npcAnimator.MoveY = 0;
        }

        npcAnimator.IsMoving = isMoving;
    }

    IEnumerator FollowPath()
    {
        while (true)
        {
            if (!isMoving)
            {
                yield return new WaitForSeconds(waitTimeAtPoint);
                isMoving = true;
            }

            while (isMoving)
            {
                Vector2 targetPosition = path[currentPathIndex];
                Vector2 movementDirection = (targetPosition - rb.position).normalized;
                Vector2 newPosition = rb.position + movementDirection * moveSpeed * Time.deltaTime;

                rb.MovePosition(newPosition);

                if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
                {
                    isMoving = false;
                    currentPathIndex = (currentPathIndex + 1) % path.Count;
                }

                yield return null;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Stop moving if a collision occurs
        if (isMoving)
        {
            isMoving = false;
        }
    }
}