using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    private Transform _target;
    public Vector4 BoundingLimits; // right up left down

    private float yOffset;

    // Use this for initialization
    void Start ()
    {
    }

    public void Initialize()
    {
        yOffset = transform.position.y - Target.localPosition.y;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Target != null)
            UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector2 targetPosition = transform.position;
        float xValue = targetPosition.x;
        float yValue = Target.localPosition.y;

        if (Target.localPosition.x > BoundingLimits.x && Target.localPosition.x < BoundingLimits.z)
        {
            xValue = Target.position.x;
        }
        else
        {
            xValue = transform.position.x;
        }
        
        if (Target.localPosition.y < BoundingLimits.y || Target.localPosition.y > BoundingLimits.w)
        {            
            yValue = transform.position.y;
        }
        else
        {            
            //yValue = Target.position.y; 
            yValue = Mathf.Lerp(yValue, Target.position.y, Time.deltaTime * 5);
        }
        

        //transform.position = Vector3.Lerp(transform.position, new Vector3(xValue, yValue, transform.position.z), Time.deltaTime * 5);
        transform.position = new Vector3(xValue, yValue, transform.position.z);

        //transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
    }

    public Transform Target
    {
        get { return _target; }
        set { _target = value; }
    }


}
