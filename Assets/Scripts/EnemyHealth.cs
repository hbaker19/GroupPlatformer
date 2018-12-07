using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {

    public int health = 3;
    private EnemyPhysObj enemyCode;

    private void Awake()
    {
        enemyCode = gameObject.GetComponent<EnemyPhysObj>();
    }

    void Update () {
        if (health < 0) { GameOver(); }
	}

    public void TakeDamage (int damage)
    {
        health -= damage;
        enemyCode.isStopped = true;
        enemyCode.stopTimer = 0;
        enemyCode.canAttack = false;
    }

    public void GameOver()
    {
        Destroy(gameObject);
    }
}
