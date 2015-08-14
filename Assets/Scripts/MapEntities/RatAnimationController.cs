using UnityEngine;
using System.Collections;

/// <summary>Controls the animation in the rat</summary>
/// <para>Attached to:</para>
/// <list type="table">
/// <listheader>
/// <term>Scene</term>
/// <description>GameObject</description>
/// </listheader>
/// <item><term>Multiple</term><description>Player</description></item>
/// </list>
public class RatAnimationController : EntityAnimationController
{

    public override void AnimateMovement(Vector2 direction)
    {
        //normal scale when anything but right
        if ((direction.y != 0) || (direction.x < 0))
        {
            playerAnimator.gameObject.transform.localScale = normalX;
        }

        //Up
        if (direction.y > 0f)
        {
            playerAnimator.SetInteger("direction", 1);

        }
        else if (direction.y == 0f)
        {
            playerAnimator.SetInteger("direction", 0);
        }


        //Right
        if (direction.x > 0f)
        {
        	//inverted X scale when going left
            playerAnimator.gameObject.transform.localScale = invertedX;
            playerAnimator.SetInteger("direction", 2);
        }
        else if (direction.x == 0f)
        {
            playerAnimator.SetInteger("direction", 0);
        }

        //Down
        if (direction.y < 0f)
        {
            playerAnimator.SetInteger("direction", 3);
        }
        else if (direction.y == 0f)
        {
            playerAnimator.SetInteger("direction", 0);
        }

        //Left
        if ((direction.x < 0f) && (direction.y == 0))
        {
            playerAnimator.SetInteger("direction", 4);
        }
        else if (direction.x == 0f)
        {
            playerAnimator.SetInteger("direction", 0);
        }
    }
}
