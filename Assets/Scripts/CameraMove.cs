using UnityEngine;
using System.Collections;


public enum LerpType
{
    Sin,
    Cos,
    Exp,
    Smooth,
    SmoothMod
};

public class CameraMove : MonoBehaviour {

    public Transform[] cameraWaypoints;
    public Transform player;
    public LerpType myLerp;

    private float currentLerp;
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
        transform.position = currentCameraPosition;
        transform.LookAt(player);
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
            currentLerp = 0;
        }

    }

    void MoveCamera()
    {
        if (currentLerp < lerpTime)
        {
            currentLerp += Time.deltaTime;
        }
        else
        {
            currentLerp = lerpTime;
            moveNext = false;
            currentCameraPosition = transform.position;
        }

        float perc = currentLerp / lerpTime;
        switch (myLerp)
        {
            case LerpType.Sin:
                perc = Mathf.Sin(perc * Mathf.PI * 0.5f);
                break;
            case LerpType.Cos:
                perc = 1f - Mathf.Cos(perc * Mathf.PI * 0.5f);
                break;
            case LerpType.Exp:
                perc = perc * perc;
                break;
            case LerpType.Smooth:
                perc = perc * perc * (3f - 2f * perc);
                break;
            case LerpType.SmoothMod:
                perc = perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                break;
        }
        perc = 1f - Mathf.Cos(perc * Mathf.PI * 0.5f);
        //transform.position = Vector3.Lerp(startPosition, actualDestination, perc);
        transform.position = Vector3.Lerp(currentCameraPosition, nextCameraPosition, perc);
        
    }
}
