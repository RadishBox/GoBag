using UnityEngine;
using System.Collections;

public abstract class EntityAnimationController : MonoBehaviour
{

    public Animator playerAnimator;

    protected Vector3 invertedX = new Vector3(-1f, 1f, 1f);
    protected Vector3 normalX = new Vector3(1f, 1f, 1f);

    private Vector2 _inputDir = Vector2.zero;

    public abstract void AnimateMovement();

    public Vector2 InputDir
    {
        get { return _inputDir; }
        set { _inputDir = value; }
    }
    
}
