using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private Transform _target;

	// Use this for initialization
	void Start () {
	
	}

    public void Initialize()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Target != null)
            transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
	}

    public Transform Target
    {
        get { return _target; }
        set { _target = value; }
    }


}
