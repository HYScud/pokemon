using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAnimator : MonoBehaviour
{

    private float moveY;
    private float moveX;
    private bool isMoving;
    private MoveDirection currentDir=MoveDirection.Down;

    [SerializeField] public List<Sprite> DownSprites { get; set; }
    [SerializeField] public List<Sprite> UpSprites { get; set; }
    [SerializeField] public List<Sprite> LeftSprites { get; set; }
    [SerializeField] public List<Sprite> RightSprites { get; set; }
    public float MoveY { get => moveY; set => moveY = value; }
    public float MoveX { get => moveX; set => moveX = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }

    SpriteAnimator walkDownAnim;
    SpriteAnimator walkUpAnim;
    SpriteAnimator walkLeftAnim;
    SpriteAnimator walkRightAnim;

    SpriteAnimator currentAnim;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            walkDownAnim = new SpriteAnimator(spriteRenderer, DownSprites);
            walkUpAnim = new SpriteAnimator(spriteRenderer, UpSprites);
            walkLeftAnim = new SpriteAnimator(spriteRenderer, LeftSprites);
            walkRightAnim = new SpriteAnimator(spriteRenderer, RightSprites);
        }
        else
        {
            Debug.Log("Error spriteRenderer is null");
        }

    }


    void Update()
    {
        this.ChangeAnimator();
        if (IsMoving)
        {
            currentAnim.HandleUpdate();
        }
        else
        {
            if (currentAnim != null)
            {
                currentAnim.SetDefaultSprite();
            }
            else
            {
                Debug.LogError("set default sprite Error currentAnim is null");
            }
        }
    }

    private void ChangeAnimator()
    {
        if (MoveX == 1)
        {
            currentAnim = walkRightAnim;
            currentDir = MoveDirection.Right;
        }
        else if (MoveX == -1)
        {
            currentAnim = walkLeftAnim;
            currentDir = MoveDirection.Left;
        }
        else if (MoveY == 1)
        {
            currentAnim = walkUpAnim;
            currentDir = MoveDirection.Up;
        }
        else
        {
            currentAnim = walkDownAnim;
            currentDir = MoveDirection.Down;
        }
    }

    public void SetRoleFaceDir(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Up:
                walkUpAnim.SetDefaultSprite();
                break;
            case MoveDirection.Down:
                walkDownAnim.SetDefaultSprite();
                break;
            case MoveDirection.Left:
                walkLeftAnim.SetDefaultSprite();
                break;
            case MoveDirection.Right:
                walkRightAnim.SetDefaultSprite();
                break;
            default:
                break;
        }
    }
}
