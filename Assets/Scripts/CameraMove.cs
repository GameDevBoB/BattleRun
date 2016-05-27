using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public Transform[] cameraWaypoints;
    public Transform player;
    private Vector3 currentCameraPosition;
    private Vector3 nextCameraPosition;
    private int index;
    private bool moveNext;
    public float lerpTime=2;
    private float clickTime;
    

    // Use this for initialization
    void Start () {
        //currentCameraPosition = transform;
        nextCameraPosition = cameraWaypoints[1].position;
        currentCameraPosition = cameraWaypoints[0].position;

        index = 1;
	}
	
	// Update is called once per frame
	void Update () {

        if (moveNext)
        {
            MoveCamera();
            transform.LookAt(player);
        }
        
	
	}

    public void CheckWaypoint()
    {
        if (index <= 2)
        {
            nextCameraPosition = cameraWaypoints[index].position;
            index++;
            clickTime = Time.time;
            moveNext = true;
        }

    }

    void MoveCamera()
    {
        if ((Time.time - clickTime) <= lerpTime) {
            transform.position = Vector3.Lerp(currentCameraPosition, nextCameraPosition, ((Time.time - clickTime) / lerpTime));
        }
        else
        {
            moveNext = false;
            currentCameraPosition = transform.position;
        }
    }
}
