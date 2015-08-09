using UnityEngine;
using System.Collections;

public class PabloControllerTest : MonoBehaviour {

	public Animator playerAnimator;
	public GameObject umbrella;
	public GameObject mask;
	public GameObject boots;
	public GameObject runningShoes;


	public Animator raincoatAnimator;
	public Animator maskAnimator;
	public Animator bootsAnimator;
	public Animator runningShoesAnimator;

	private Vector3 invertedX;
	private Vector3 normalX;

	// Use this for initialization
	void Start () {

		invertedX = new Vector3 (-1f, 1f, 1f);
		normalX = new Vector3 (1f, 1f, 1f);
	}

	// Update is called once per frame
	void Update () {

		//normal scale when anything but left
		if ((Input.GetAxis ("Vertical") != 0) || (Input.GetAxis ("Horizontal") > 0)) {
			this.transform.localScale = normalX;
			mask.transform.localScale = normalX;
			umbrella.transform.localScale = normalX;
			boots.transform.localScale = normalX;
			runningShoes.transform.localScale = normalX;
		}

        
		//Up
		if (Input.GetAxis ("Vertical") > 0f){
			playerAnimator.SetBool ("playerIsUp", true);
			raincoatAnimator.SetBool ("umbrellaIsUp", true);
			maskAnimator.SetBool ("maskIsUp", true);
			bootsAnimator.SetBool ("bootsIsUp", true);
			runningShoesAnimator.SetBool ("runningShoesIsUp", true);

		} 
		else if (Input.GetAxis ("Vertical") ==0f){
			playerAnimator.SetBool ("playerIsUp", false);     
            raincoatAnimator.SetBool("umbrellaIsUp", false);
            maskAnimator.SetBool("maskIsUp", false);
            bootsAnimator.SetBool("bootsIsUp", false);
            runningShoesAnimator.SetBool("runningShoesIsUp", false);
		}

        
		//Right
		//else ifs? solo hay uno a la vez
		if (Input.GetAxis ("Horizontal") > 0f) {
			playerAnimator.SetBool ("playerIsRight", true);
			raincoatAnimator.SetBool ("umbrellaIsRight", true);
			maskAnimator.SetBool ("maskIsRight", true);
			bootsAnimator.SetBool ("bootsIsRight", true);
			runningShoesAnimator.SetBool ("runningShoesIsRight", true);
		} 
		else if (Input.GetAxis ("Horizontal") == 0f) {
            playerAnimator.SetBool("playerIsRight", false);
            raincoatAnimator.SetBool("umbrellaIsRight", false);
            maskAnimator.SetBool("maskIsRight", false);
            bootsAnimator.SetBool("bootsIsRight", false);
            runningShoesAnimator.SetBool("runningShoesIsRight", false);
		}

		//Down
		if (Input.GetAxis ("Vertical") < 0f) {
			playerAnimator.SetBool ("playerIsDown", true);
			raincoatAnimator.SetBool ("umbrellaIsDown", true);
			maskAnimator.SetBool ("maskIsDown", true);
			bootsAnimator.SetBool ("bootsIsDown", true);
			runningShoesAnimator.SetBool ("runningShoesIsDown", true);
		} 
		else if (Input.GetAxis ("Vertical") == 0f) {
            playerAnimator.SetBool("playerIsDown", false);
            raincoatAnimator.SetBool("umbrellaIsDown", false);
            maskAnimator.SetBool("maskIsDown", false);
            bootsAnimator.SetBool("bootsIsDown", false);
            runningShoesAnimator.SetBool("runningShoesIsDown", false);
		}

		//Left
		if ((Input.GetAxis ("Horizontal") < 0f) && (Input.GetAxis ("Vertical")==0)) {
			//inverted X scale when going left
			this.transform.localScale = invertedX;

			playerAnimator.SetBool ("playerIsLeft", true);
			raincoatAnimator.SetBool ("umbrellaIsLeft", true);
			maskAnimator.SetBool ("maskIsLeft", true);
			bootsAnimator.SetBool ("bootsIsLeft", true);
			runningShoesAnimator.SetBool ("runningShoesIsLeft", true);
		} 
		else if (Input.GetAxis ("Horizontal") == 0f) {
            playerAnimator.SetBool("playerIsLeft", false);
            raincoatAnimator.SetBool("umbrellaIsLeft", false);
            maskAnimator.SetBool("maskIsLeft", false);
            bootsAnimator.SetBool("bootsIsLeft", false);
            runningShoesAnimator.SetBool("runningShoesIsLeft", false);
		}
	}
}
