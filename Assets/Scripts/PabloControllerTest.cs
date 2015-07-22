using UnityEngine;
using System.Collections;

public class PabloControllerTest : MonoBehaviour {

	Animator playerAnimator;
	public GameObject umbrella;
	public GameObject mask;
	public GameObject boots;
	public GameObject runningShoes;


	Animator umbrellaAnimator;
	Animator maskAnimator;
	Animator bootsAnimator;
	Animator runningShoesAnimator;

	private Vector3 invertedX;
	private Vector3 normalX;

	// Use this for initialization
	void Start () {
		playerAnimator = this.GetComponent<Animator>();
		umbrellaAnimator = umbrella.GetComponent<Animator> ();
		maskAnimator = mask.GetComponent<Animator> ();
		bootsAnimator = boots.GetComponent<Animator> ();
		runningShoesAnimator = runningShoes.GetComponent<Animator> ();

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
			umbrellaAnimator.SetBool ("umbrellaIsUp", true);
			maskAnimator.SetBool ("maskIsUp", true);
			bootsAnimator.SetBool ("bootsIsUp", true);
			runningShoesAnimator.SetBool ("runningShoesIsUp", true);

		} 
		else if (Input.GetAxis ("Vertical") ==0f){
			playerAnimator.SetBool ("playerIsUp", false);
            umbrellaAnimator.SetBool("umbrellaIsUp", false);
            maskAnimator.SetBool("maskIsUp", false);
            bootsAnimator.SetBool("bootsIsUp", false);
            runningShoesAnimator.SetBool("runningShoesIsUp", false);
		}

		//Right
		//else ifs? solo hay uno a la vez
		if (Input.GetAxis ("Horizontal") > 0f) {
			playerAnimator.SetBool ("playerIsRight", true);
			umbrellaAnimator.SetBool ("umbrellaIsRight", true);
			maskAnimator.SetBool ("maskIsRight", true);
			bootsAnimator.SetBool ("bootsIsRight", true);
			runningShoesAnimator.SetBool ("runningShoesIsRight", true);
		} 
		else if (Input.GetAxis ("Horizontal") == 0f) {
            playerAnimator.SetBool("playerIsRight", false);
            umbrellaAnimator.SetBool("umbrellaIsRight", false);
            maskAnimator.SetBool("maskIsRight", false);
            bootsAnimator.SetBool("bootsIsRight", false);
            runningShoesAnimator.SetBool("runningShoesIsRight", false);
		}

		//Down
		if (Input.GetAxis ("Vertical") < 0f) {
			playerAnimator.SetBool ("playerIsDown", true);
			umbrellaAnimator.SetBool ("umbrellaIsDown", true);
			maskAnimator.SetBool ("maskIsDown", true);
			bootsAnimator.SetBool ("bootsIsDown", true);
			runningShoesAnimator.SetBool ("runningShoesIsDown", true);
		} 
		else if (Input.GetAxis ("Vertical") == 0f) {
            playerAnimator.SetBool("playerIsDown", false);
            umbrellaAnimator.SetBool("umbrellaIsDown", false);
            maskAnimator.SetBool("maskIsDown", false);
            bootsAnimator.SetBool("bootsIsDown", false);
            runningShoesAnimator.SetBool("runningShoesIsDown", false);
		}

		//Left
		if ((Input.GetAxis ("Horizontal") < 0f) && (Input.GetAxis ("Vertical")==0)) {
			//inverted X scale when going left
			this.transform.localScale = invertedX;
			mask.transform.localScale = invertedX;
			umbrella.transform.localScale = invertedX;
			boots.transform.localScale = invertedX;
			runningShoes.transform.localScale = invertedX;

			playerAnimator.SetBool ("playerIsLeft", true);
			umbrellaAnimator.SetBool ("umbrellaIsLeft", true);
			maskAnimator.SetBool ("maskIsLeft", true);
			bootsAnimator.SetBool ("bootsIsLeft", true);
			runningShoesAnimator.SetBool ("runningShoesIsLeft", true);
		} 
		else if (Input.GetAxis ("Horizontal") == 0f) {
            playerAnimator.SetBool("playerIsLeft", false);
            umbrellaAnimator.SetBool("umbrellaIsLeft", false);
            maskAnimator.SetBool("maskIsLeft", false);
            bootsAnimator.SetBool("bootsIsLeft", false);
            runningShoesAnimator.SetBool("runningShoesIsLeft", false);
		}
	}
}
