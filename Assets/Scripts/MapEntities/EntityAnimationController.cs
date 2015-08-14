using UnityEngine;
using System.Collections;

public abstract class EntityAnimationController : MonoBehaviour
{

    public Animator playerAnimator;
    public SpriteRenderer spriteRenderer;

    protected Vector3 invertedX = new Vector3(-1f, 1f, 1f);
    protected Vector3 normalX = new Vector3(1f, 1f, 1f);

    public abstract void AnimateMovement(Vector2 direction);

    public float AnimatorSpeed
    {
        get { return playerAnimator.speed; }
        set { playerAnimator.speed = value; }
    }

    public Sprite Sprite
    {
        get { return spriteRenderer.sprite; }
        set { spriteRenderer.sprite = value; }
    }
}
