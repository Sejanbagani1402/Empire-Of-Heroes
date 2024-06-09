using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCcontroller : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] List<Vector2> path; // Predefined path points for the NPC to follow
    [SerializeField] float waitTimeAtPoint = 1.0f;

    NPCAnimator npcAnimator;
    int currentPathIndex = 0;
    bool isMoving = false;

    void Start()
    {
        npcAnimator = GetComponent<NPCAnimator>();
        StartCoroutine(FollowPath());
    }

    void Update()
    {
        // Update NPCAnimator with movement values
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
                Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

                if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
                {
                    isMoving = false;
                    currentPathIndex = (currentPathIndex + 1) % path.Count;
                }

                yield return null;
            }
        }
    }
}
