using UnityEngine;
using System.Collections;

public class PabloControllerTest : MonoBehaviour {

	Animator anim;
	public Animator umbrella;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Vertical") > 0f) {
			anim.SetBool ("isUp", true);
			umbrella.SetBool ("isUp2", true);
		} else if (Input.GetAxis ("Vertical") < 1f) {
			anim.SetBool ("isUp", false);
			umbrella.SetBool ("isUp2", false);
		}
	}
}
