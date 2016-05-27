using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public Transform[] cameraWaypoints;
    public Transform player;
    private Transform currentCameraPosition;
    private Transform nextCameraPosition;
    private int index;
    private bool moveNext;
    public float lerpTime=2;
    private float clickTime;
    

    // Use this for initialization
    void Start () {
        currentCameraPosition = transform;
        nextCameraPosition = cameraWaypoints[1];
        currentCameraPosition.position = cameraWaypoints[0].position;
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
            nextCameraPosition.position = cameraWaypoints[index].position;
            index++;
            clickTime = Time.time;
            moveNext = true;
        }

    }

    void MoveCamera()
    {
        if ((Time.time - clickTime) <= lerpTime)
            transform.position = Vector3.Lerp(currentCameraPosition.position, nextCameraPosition.position, ((Time.time - clickTime)/lerpTime));
        else
        {
            moveNext = false;
            currentCameraPosition.position = transform.position;
        }
    }
}
