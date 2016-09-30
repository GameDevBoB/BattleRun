using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Transform[] wayPoints;
    public float lerpTime;

    private int counter;

    // Use this for initialization
    void Start () {
        counter=0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartRunning()
    {
        StartCoroutine("Run");
    }

    IEnumerator Run()
    {
        float time = 0;
        while(time < lerpTime)
        {
            transform.position = Vector3.Lerp(wayPoints[counter].position, wayPoints[counter + 1].position, time / lerpTime);
            time += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        counter++;
    }
}
