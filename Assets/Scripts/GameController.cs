using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum moves
{
    attack1,
    attack2,
    attack3,
    attack4,
    attack5,
    attack6,
    attack7,
    attack8
}
public class GameController : MonoBehaviour
{

    public CameraMove myCameraScript;
    public List<moves> moveSet;
    public float minTimeDuration;
    public float maxTimeDuration;
    public float minWaitDuration;
    public float maxWaitDuration;
    public GameObject quickTimeObject;
    public static GameController instance;


    private float waitDuration;
    private float startWait;
    private int moveSetCounter;
	//private bool waveExist;
    // Use this for initialization

    void Awake()
    {
        instance = this;
        waitDuration = Random.Range(minWaitDuration, maxWaitDuration);
        moveSetCounter = 0;
        startWait = 0;
		//waveExist = true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TEST
        if (moveSetCounter == moveSet.Count) {
			//waveExist = false;
			myCameraScript.CheckWaypoint();
		} else {
			//TEST
			if ((Time.time - startWait) > waitDuration && !quickTimeObject.activeSelf && moveSetCounter < moveSet.Count) {

				ActivateQuicktime ();
			}
			if (Input.anyKeyDown && quickTimeObject.activeSelf) {
				CheckInput ();
				quickTimeObject.SendMessage ("Deactivate");
			}
		}


    }

	public void SpawnWave()
	{
		Debug.Log("SET DI NEMICI SPAWNATO, PROSSIMO SET");
		moveSetCounter = 0;
		RandomizeMove();
		//waveExist = true;
	}

    public void MoveCamera()
    {
        myCameraScript.CheckWaypoint();
    }

    private void ActivateQuicktime()
    {
        quickTimeObject.SetActive(true);
        quickTimeObject.SendMessage("StartQuickTimeEvent", Random.Range(minTimeDuration, maxTimeDuration));
        quickTimeObject.SendMessage("SetMove", moveSet[moveSetCounter]);



    }
    public void QuickTimeWaitTimer()
    {
        startWait = Time.time;
        waitDuration = Random.Range(minWaitDuration, maxWaitDuration);
        moveSetCounter++;
    }

    void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && moveSet[moveSetCounter] == moves.attack1)
        {

            Debug.Log("BRAVO!");

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && moveSet[moveSetCounter] == moves.attack2)
        {

            Debug.Log("BRAVO!");

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && moveSet[moveSetCounter] == moves.attack3)
        {

            Debug.Log("BRAVO!");

        }
        else
        {
            Debug.Log("QUICKTIME EVENT FALLITO");
        }

    }

    void RandomizeMove()
    {
        int cicles = Random.Range(0, 100);
        for(int i = 0; i < cicles; i++)
        {
            int cont1 = Random.Range(0, moveSet.Count);
            int cont2 = Random.Range(0, moveSet.Count);
            moves aux;
            aux = moveSet[cont1];
            moveSet[cont1] = moveSet[cont2];
            moveSet[cont2] = aux;
        }
    }
}
