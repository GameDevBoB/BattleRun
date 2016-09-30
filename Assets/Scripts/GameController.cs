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
    public int enemyInStageNumber;
    public float minTimeDuration;
    public float maxTimeDuration;
    public float minWaitDuration;
    public float maxWaitDuration;
    public static GameController instance;
    public Transform[] enemySpawnPoints;
    public GameObject player;

    private float waitDuration;
    private float startWait;
    //private int enemyInStageCounter;
    private Moves actualMove;
    private GameObject quickTimeObject;
    //private int moveSetCounter;
    private List<GameObject> enemyInStage;
    private int enemySpawnPointCounter;
    private int movesCount;
	//private bool waveExist;
    // Use this for initialization

    void Awake()
    {
        instance = this;
        waitDuration = Random.Range(minWaitDuration, maxWaitDuration);
        //moveSetCounter = 0;
        //enemyInStageCounter = 0;
        startWait = 0;
		//waveExist = true;
        movesCount = System.Enum.GetNames(typeof(Moves)).Length;
        Time.timeScale = 1;
        //Debug.Log("pincopallino " + pincopallino);
    }
    void Start()
    {
        enemySpawnPointCounter = 0;
        enemyInStage = new List<GameObject>();
        SpawnEnemies();
        RandomizeEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        //TEST
        if (enemyInStage.Count == 0) {
			//waveExist = false;
			myCameraScript.CheckWaypoint();
		} else {
			//TEST
			if ((Time.time - startWait) > waitDuration && (!quickTimeObject || !quickTimeObject.activeSelf) && enemyInStage.Count > 0) {
                quickTimeObject = enemyInStage[0].transform.GetChild(0).GetChild(0).gameObject;
				ActivateQuicktime ();
			}
			/*if (Input.anyKeyDown && quickTimeObject.activeSelf) {
                quickTimeObject.SendMessage("Deactivate");
                CheckInput ();
                CancelInvoke("GetDamage");
            }*/
		}


    }

    void SpawnEnemies()
    {
        GameObject enemy;

        for(int i = 0; i < enemyInStageNumber; i++)
        {
            //Debug.Log(enemySpawnPoints[enemySpawnPointCounter]);
            enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], enemySpawnPoints[enemySpawnPointCounter].position, Quaternion.identity) as GameObject;
            enemyInStage.Add(enemy);
            enemySpawnPointCounter++;
        }
    }

    void RandomizeEnemies()
    {
        int cicles = Random.Range(0, 100);
        GameObject aux;
        for (int i = 0; i < cicles; i++)
        {
            int cont1 = Random.Range(0, enemyInStage.Count);
            int cont2 = Random.Range(0, enemyInStage.Count);
            aux = enemyInStage[cont1];
            enemyInStage[cont1] = enemyInStage[cont2];
            enemyInStage[cont2] = aux;
        }
    }

    public void SpawnWave()
	{
		//Debug.Log("SET DI NEMICI SPAWNATO, PROSSIMO SET");
		//enemyInStageCounter = 0;
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
        Invoke("Damage", time);
        actualMove = (Moves) Random.Range(0, movesCount);
        quickTimeObject.SendMessage("SetMove", actualMove);
        //Debug.Log(actualMove);



    }
    public void QuickTimeWaitTimer()
    {
        startWait = Time.time;
        waitDuration = Random.Range(minWaitDuration, maxWaitDuration);
    }

    public void CheckInput(int pressedButton)
    {
        if (quickTimeObject.activeSelf)
        {
            quickTimeObject.SendMessage("Deactivate");
            if (pressedButton == 0 && actualMove == Moves.attack1)
            {
                //Debug.Log("BRAVO!");
                enemyInStage[0].SendMessage("Die");
                enemyInStage.RemoveAt(0);
                //enemyInStageCounter++;
            }
            else if (pressedButton == 1 && actualMove == Moves.attack2)
            {
                //Debug.Log("BRAVO!");
                enemyInStage[0].SendMessage("Die");
                enemyInStage.RemoveAt(0);
                //enemyInStageCounter++;
            }
            else if (pressedButton == 2 && actualMove == Moves.attack3)
            {
                //Debug.Log("BRAVO!");
                enemyInStage[0].SendMessage("Die");
                enemyInStage.RemoveAt(0);
                //enemyInStageCounter++;
            }
            else
            {
                //Debug.Log("QUICKTIME EVENT FALLITO");
                Damage();
            }
            CancelInvoke("Damage");
        }      
    }

    void ClearStage()
    {
        foreach (GameObject enemy in enemyInStage)
        {
            Destroy(enemy);
        }
        enemyInStage.Clear();
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

    void Damage()
    {
        //Debug.Log("Danno!!!!");
        enemyInStage[0].SendMessage("DamagePlayer");
        RandomizeEnemies();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }

}
