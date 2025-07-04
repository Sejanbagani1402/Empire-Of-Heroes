using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    [SerializeField] List<Sprite> walkDownSprites;
    [SerializeField] List<Sprite> walkUpSprites;
    [SerializeField] List<Sprite> walkRightSprites;
    [SerializeField] List<Sprite> walkLeftSprites;
    [SerializeField] Sprite idealSprite;

    [SerializeField] Vector3 movingScale = new Vector3(1, 1, 1);

    // Parameters
    public float MoveX { get; set; }
    public float MoveY { get; set; }
    public bool IsMoving { get; set; }

    // States
    SpriteAnimator walkDownAnim;
    SpriteAnimator walkUpAnim;
    SpriteAnimator walkRightAnim;
    SpriteAnimator walkLeftAnim;

    SpriteAnimator currentAnim;

    // Reference
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        walkDownAnim = new SpriteAnimator(walkDownSprites, spriteRenderer);
        walkUpAnim = new SpriteAnimator(walkUpSprites, spriteRenderer);
        walkRightAnim = new SpriteAnimator(walkRightSprites, spriteRenderer);
        walkLeftAnim = new SpriteAnimator(walkLeftSprites, spriteRenderer);

        currentAnim = walkDownAnim;
    }

    private void Update()
    {
        var prevAnim = currentAnim;

        // Determine the animation based on the movement direction
        if (Mathf.Abs(MoveX) > Mathf.Abs(MoveY))
        {
            if (MoveX > 0)
                currentAnim = walkRightAnim;
            else if (MoveX < 0)
                currentAnim = walkLeftAnim;
        }
        else
        {
            if (MoveY > 0)
                currentAnim = walkUpAnim;
            else if (MoveY < 0)
                currentAnim = walkDownAnim;
        }

        if (currentAnim != prevAnim)
        {
            currentAnim.Start();
        }

        if (IsMoving)
        {
            currentAnim.Update();
            transform.localScale = movingScale;
            idealSprite = currentAnim.CurrentSprite;  // Store the current frame as the idle sprite
        }
        else
        {
            spriteRenderer.sprite = idealSprite;  // Use the stored frame when idle
        }
    }
}
