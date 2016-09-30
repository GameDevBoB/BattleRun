using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuickTimeEvent : MonoBehaviour
{

    public Slider quickTimeRing;
    public Sprite[] moveKeys;
    public Image moveImage;
    public Canvas canvasQte;


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

    void LateUpdate()
    {

        canvasQte.transform.LookAt(canvasQte.transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.down);
        canvasQte.transform.Rotate(Vector3.right * 180);
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
        quickTimeRing.value = quickTimeRing.minValue + ((Time.time - startTime) / (duration * quickTimeRing.maxValue));
    }

    public void SetMove(Moves newMove)
    {
        switch (newMove)
        {
            case Moves.attack1:
                moveImage.sprite = moveKeys[0];
                break;
            case Moves.attack2:
                moveImage.sprite = moveKeys[1];
                break;
            case Moves.attack3:
                moveImage.sprite = moveKeys[2];
                break;
            //case moves.attack4:
                //break;
            //case moves.attack5:
                //break;
            //case moves.attack6:
                //break;
            //case moves.attack7:
                //break;
            //case moves.attack8:
                //break;

        }
    }
}
