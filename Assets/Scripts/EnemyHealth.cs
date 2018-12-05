using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health = 3;
    
	void Update () {
        if (health < 0) { GameOver(); }
	}

    public void GameOver()
    {
        Destroy(gameObject);
    }
}
