using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    public static GuiController instance;
    public Slider healthBar;
    public Text gameOverText;
   
    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetHealthBar(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void UpdateHealthBar(float actualHealth)
    {
        healthBar.value = actualHealth;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }
}
