using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void DamagePlayer()
    {
        GameController.instance.player.SendMessage("GetDamage", damage);
    }
}
