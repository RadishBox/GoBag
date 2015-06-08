using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BagItem : MonoBehaviour {

    private int _id;
    private string _name;

    private int[,] _shape;
    private float _rotation;

    private RectTransform rectTransform;

    // Numer of times, if consumable

	// Use this for initialization
	void Start () {
        rectTransform = this.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
