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
public class PlayerAnimationController : EntityAnimationController {

	public GameObject raincoat;
    public GameObject mask;
    public GameObject boots;
    public GameObject runningShoes;

    public Animator raincoatAnimator;
    public Animator maskAnimator;
    public Animator bootsAnimator;
    public Animator runningShoesAnimator;

	public override void AnimateMovement()
    {
        //normal scale when anything but left
        if ((InputDir.y != 0) || (InputDir.x > 0))
        {
            playerAnimator.gameObject.transform.localScale = normalX;
            mask.transform.localScale = normalX;
            raincoat.transform.localScale = normalX;
            boots.transform.localScale = normalX;
            runningShoes.transform.localScale = normalX;
        }

        //Up
        if (InputDir.y > 0f)
        {
            playerAnimator.SetBool("playerIsUp", true);
            raincoatAnimator.SetBool("umbrellaIsUp", true);
            maskAnimator.SetBool("maskIsUp", true);
            bootsAnimator.SetBool("bootsIsUp", true);
            runningShoesAnimator.SetBool("runningShoesIsUp", true);

        }
        else if (InputDir.y == 0f)
        {
            playerAnimator.SetBool("playerIsUp", false);
            raincoatAnimator.SetBool("umbrellaIsUp", false);
            maskAnimator.SetBool("maskIsUp", false);
            bootsAnimator.SetBool("bootsIsUp", false);
            runningShoesAnimator.SetBool("runningShoesIsUp", false);
        }


        //Right
        //else ifs? solo hay uno a la vez
        if (InputDir.x > 0f)
        {
            playerAnimator.SetBool("playerIsRight", true);
            raincoatAnimator.SetBool("umbrellaIsRight", true);
            maskAnimator.SetBool("maskIsRight", true);
            bootsAnimator.SetBool("bootsIsRight", true);
            runningShoesAnimator.SetBool("runningShoesIsRight", true);
        }
        else if (InputDir.x == 0f)
        {
            playerAnimator.SetBool("playerIsRight", false);
            raincoatAnimator.SetBool("umbrellaIsRight", false);
            maskAnimator.SetBool("maskIsRight", false);
            bootsAnimator.SetBool("bootsIsRight", false);
            runningShoesAnimator.SetBool("runningShoesIsRight", false);
        }

        //Down
        if (InputDir.y < 0f)
        {
            playerAnimator.SetBool("playerIsDown", true);
            raincoatAnimator.SetBool("umbrellaIsDown", true);
            maskAnimator.SetBool("maskIsDown", true);
            bootsAnimator.SetBool("bootsIsDown", true);
            runningShoesAnimator.SetBool("runningShoesIsDown", true);
        }
        else if (InputDir.y == 0f)
        {
            playerAnimator.SetBool("playerIsDown", false);
            raincoatAnimator.SetBool("umbrellaIsDown", false);
            maskAnimator.SetBool("maskIsDown", false);
            bootsAnimator.SetBool("bootsIsDown", false);
            runningShoesAnimator.SetBool("runningShoesIsDown", false);
        }

        //Left
        if ((InputDir.x < 0f) && (InputDir.y == 0))
        {
            //inverted X scale when going left
            playerAnimator.gameObject.transform.localScale = invertedX;
            raincoat.transform.localScale = invertedX;
            mask.transform.localScale = invertedX;
            bootsAnimator.transform.localScale = invertedX;
            runningShoes.transform.localScale = invertedX;


            playerAnimator.SetBool("playerIsLeft", true);
            raincoatAnimator.SetBool("umbrellaIsLeft", true);
            maskAnimator.SetBool("maskIsLeft", true);
            bootsAnimator.SetBool("bootsIsLeft", true);
            runningShoesAnimator.SetBool("runningShoesIsLeft", true);
        }
        else if (InputDir.x == 0f)
        {
            playerAnimator.SetBool("playerIsLeft", false);
            raincoatAnimator.SetBool("umbrellaIsLeft", false);
            maskAnimator.SetBool("maskIsLeft", false);
            bootsAnimator.SetBool("bootsIsLeft", false);
            runningShoesAnimator.SetBool("runningShoesIsLeft", false);
        }
    }
}
