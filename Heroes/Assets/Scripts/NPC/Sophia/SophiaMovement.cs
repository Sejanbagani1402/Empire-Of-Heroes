using Unity.VisualScripting;
using UnityEngine;

public class SophiaMovement : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float moveSpeed = 2f;  // Speed at which the NPC moves
    public float stopDistance = 1.5f;  // Distance at which the NPC stops

    private Rigidbody2D rb;  // Reference to the NPC's Rigidbody2D
    private Vector2 movement;  // Direction of movement
    private NPCAnimator npcAnimator;  // Reference to the NPCAnimator script
    private bool isConversationStarted = false;

    public DialogueTrigger dialogueTrigger;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        npcAnimator = GetComponent<NPCAnimator>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction;

            // Update NPCAnimator properties
            npcAnimator.MoveX = direction.x;
            npcAnimator.MoveY = direction.y;
            npcAnimator.IsMoving = true;
        }
        else
        {
            movement = Vector2.zero;

            // Update NPCAnimator properties
            npcAnimator.IsMoving = false;
            if (!isConversationStarted)
            {
                StartConversation();
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    void StartConversation()
    {
        if(dialogueTrigger != null)
        {
            dialogueTrigger.StartDialoque();    
            isConversationStarted = true;
        }
    }
}
