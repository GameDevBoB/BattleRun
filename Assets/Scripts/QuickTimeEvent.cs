using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuickTimeEvent : MonoBehaviour
{

    public Slider quickTimeRing;
    public string[] moveKeys;
    public Text moveText;


    private float duration;
    private float startTime;
    //private moves actualMove;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if ((Time.time - startTime) > duration)
        {
            Deactivate();
        }
        DoQuickTime();

    }

    public void StartQuickTimeEvent(float newDuration)
    {
        duration = newDuration;
        startTime = Time.time;

    }

    void Deactivate()
    {
        gameObject.SetActive(false);
        GameController.instance.QuickTimeWaitTimer();

    }

    void DoQuickTime()
    {
        quickTimeRing.value = quickTimeRing.maxValue - ((Time.time - startTime) / (duration * quickTimeRing.maxValue));
    }

    public void SetMove(moves newMove)
    {
        switch (newMove)
        {
            case moves.attack1:
                moveText.text = moveKeys[0];
                break;
            case moves.attack2:
                moveText.text = moveKeys[1];
                break;
            case moves.attack3:
                moveText.text = moveKeys[2];
                break;
            case moves.attack4:
                break;
            case moves.attack5:
                break;
            case moves.attack6:
                break;
            case moves.attack7:
                break;
            case moves.attack8:
                break;

        }
    }
}
