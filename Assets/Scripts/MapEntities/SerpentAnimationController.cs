using UnityEngine;
using System.Collections;

/// <summary>Controls the animation in the player</summary>
/// <para>Attached to:</para>
/// <list type="table">
/// <listheader>
/// <term>Scene</term>
/// <description>GameObject</description>
/// </listheader>
/// <item><term>Multiple</term><description>Player</description></item>
/// </list>
public class SerpentAnimationController : EntityAnimationController {

	public override void AnimateMovement()
    {
        //normal scale when anything but left
        if ((InputDir.y != 0) || (InputDir.x > 0))
        {
            playerAnimator.gameObject.transform.localScale = normalX;
        }

        //Up
        if (InputDir.y > 0f)
        {
            playerAnimator.SetBool("playerIsUp", true);
        }
        else if (InputDir.y == 0f)
        {
            playerAnimator.SetBool("playerIsUp", false);
        }


        //Right
        //else ifs? solo hay uno a la vez
        if (InputDir.x > 0f)
        {
            playerAnimator.SetBool("playerIsRight", true);
        }
        else if (InputDir.x == 0f)
        {
            playerAnimator.SetBool("playerIsRight", false);
        }

        //Down
        if (InputDir.y < 0f)
        {
            playerAnimator.SetBool("playerIsDown", true);
        }
        else if (InputDir.y == 0f)
        {
            playerAnimator.SetBool("playerIsDown", false);
        }

        //Left
        if ((InputDir.x < 0f) && (InputDir.y == 0))
        {
            //inverted X scale when going left
            playerAnimator.gameObject.transform.localScale = invertedX;
            playerAnimator.SetBool("playerIsLeft", true);
        }
        else if (InputDir.x == 0f)
        {
            playerAnimator.SetBool("playerIsLeft", false);
        }
    }
}
