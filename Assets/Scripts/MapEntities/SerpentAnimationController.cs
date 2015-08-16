using UnityEngine;
using System.Collections;

/// <summary>Controls the animation in the serpent</summary>
/// <para>Attached to:</para>
/// <list type="table">
/// <listheader>
/// <term>Scene</term>
/// <description>GameObject</description>
/// </listheader>
/// <item><term>Multiple</term><description>Player</description></item>
/// </list>
public class SerpentAnimationController : EntityAnimationController
{

    public override void AnimateMovement(Vector2 direction)
    {
        if (direction.y == 0 && direction.x == 0) // Stop
        {
            playerAnimator.SetFloat("idleSpeed", 1);
            return;
        }
        else
        {
            playerAnimator.SetFloat("idleSpeed", 15);
        }

        //normal scale when anything but left
        if ((direction.y != 0) || (direction.x > 0))
        {
            playerAnimator.gameObject.transform.localScale = normalX;
        }

        //Up rotate
        if (direction.y > 0f)
        {
            transform.eulerAngles = new Vector3(0, 0, -135);
            return;
        }

        //Right rotate
        if (direction.x > 0f)
        {
            transform.eulerAngles = new Vector3(0, 0, 135);
            return;
        }

        //Down rotate
        if (direction.y < 0f)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
            return;
        }

        //Left rotate
        if ((direction.x < 0f) && (direction.y == 0))
        {
            transform.eulerAngles  = new Vector3(0, 0, -45);
            return;
        }
    }
}
