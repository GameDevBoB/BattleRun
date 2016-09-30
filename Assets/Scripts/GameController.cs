using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System;


public enum Moves
{
    attack1,
    attack2,
    attack3,
    //attack4,
    //attack5,
    //attack6,
    //attack7,
    //attack8
}


public class GameController : MonoBehaviour
{

    public CameraMove myCameraScript;
    //public List<moves> moveSet;
    public GameObject[] enemyPrefabs;
    public int enemyStageNumber;
    public float minTimeDuration;
    public float maxTimeDuration;
    public float minWaitDuration;
    public float maxWaitDuration;
    public static GameController instance;
    public Transform[] enemySpawnPoints;
    public GameObject player;

    private float waitDuration;
    private float startWait;
    //private int enemyStageCounter;
    private Moves actualMove;
    private GameObject quickTimeObject;
    //private int moveSetCounter;
    public List<GameObject> enemyStage;
    private int enemySpawnPointCounter;
    private int movesCount;
	//private bool waveExist;
    // Use this for initialization

    void Awake()
    {
        instance = this;
        waitDuration = Random.Range(minWaitDuration, maxWaitDuration);
        //moveSetCounter = 0;
        //enemyStageCounter = 0;
        startWait = 0;
		//waveExist = true;
        movesCount = System.Enum.GetNames(typeof(Moves)).Length;
        //Debug.Log("pincopallino " + pincopallino);
    }
    void Start()
    {
        enemySpawnPointCounter = 0;
        enemyStage = new List<GameObject>();
        SpawnEnemies();
        RandomizeEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        //TEST
        if (enemyStage.Count == 0) {
			//waveExist = false;
			myCameraScript.CheckWaypoint();
		} else {
			//TEST
			if ((Time.time - startWait) > waitDuration && (!quickTimeObject || !quickTimeObject.activeSelf) && enemyStage.Count > 0) {
                quickTimeObject = enemyStage[0].transform.GetChild(0).GetChild(0).gameObject;
				ActivateQuicktime ();
			}
			if (Input.anyKeyDown && quickTimeObject.activeSelf) {
                quickTimeObject.SendMessage("Deactivate");
                CheckInput ();
                CancelInvoke("GetDamage");
            }
		}


    }

    void SpawnEnemies()
    {
        GameObject enemy;

        for(int i = 0; i < enemyStageNumber; i++)
        {
            //Debug.Log(enemySpawnPoints[enemySpawnPointCounter]);
            enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], enemySpawnPoints[enemySpawnPointCounter].position, Quaternion.identity) as GameObject;
            enemyStage.Add(enemy);
            enemySpawnPointCounter++;
        }
    }

    void RandomizeEnemies()
    {
        int cicles = Random.Range(0, 100);
        GameObject aux;
        for (int i = 0; i < cicles; i++)
        {
            int cont1 = Random.Range(0, enemyStage.Count);
            int cont2 = Random.Range(0, enemyStage.Count);
            aux = enemyStage[cont1];
            enemyStage[cont1] = enemyStage[cont2];
            enemyStage[cont2] = aux;
        }
    }

    public void SpawnWave()
	{
		Debug.Log("SET DI NEMICI SPAWNATO, PROSSIMO SET");
		//enemyStageCounter = 0;
        ClearStage();
        SpawnEnemies();
        RandomizeEnemies();
        //RandomizeMove();
        //waveExist = true;
    }

    public void MoveCamera()
    {
        myCameraScript.CheckWaypoint();
    }

    private void ActivateQuicktime()
    {
        float time;
        quickTimeObject.SetActive(true);
        quickTimeObject.SendMessage("StartQuickTimeEvent", time = Random.Range(minTimeDuration, maxTimeDuration));
        Invoke("GetDamage", time);
        actualMove = (Moves) Random.Range(0, movesCount);
        quickTimeObject.SendMessage("SetMove", actualMove);
        //Debug.Log(actualMove);



    }
    public void QuickTimeWaitTimer()
    {
        startWait = Time.time;
        waitDuration = Random.Range(minWaitDuration, maxWaitDuration);
    }

    void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && actualMove == Moves.attack1)
        {
            //Debug.Log("BRAVO!");
            enemyStage[0].SetActive(false);
            enemyStage.RemoveAt(0);
            //enemyStageCounter++;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && actualMove == Moves.attack2)
        {
            //Debug.Log("BRAVO!");
            enemyStage[0].SetActive(false);
            enemyStage.RemoveAt(0);
            //enemyStageCounter++;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && actualMove == Moves.attack3)
        {
            //Debug.Log("BRAVO!");
            enemyStage[0].SetActive(false);
            enemyStage.RemoveAt(0);
            //enemyStageCounter++;
        }
        else
        {
            Debug.Log("QUICKTIME EVENT FALLITO");
            GetDamage();
        }        
    }

    void ClearStage()
    {
        foreach (GameObject enemy in enemyStage)
        {
            Destroy(enemy);
        }
        enemyStage.Clear();
    }

    /*void RandomizeMove()
    {
        int cicles = Random.Range(0, 100);
        moves aux;
        for (int i = 0; i < cicles; i++)
        {
            int cont1 = Random.Range(0, moveSet.Count);
            int cont2 = Random.Range(0, moveSet.Count);
            aux = moveSet[cont1];
            moveSet[cont1] = moveSet[cont2];
            moveSet[cont2] = aux;
        }
    }*/

    void GetDamage()
    {
        Debug.Log("Danno!!!!");
        RandomizeEnemies();
    }

}
