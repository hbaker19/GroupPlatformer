using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {

    public int health = 3;
    private EnemyPhysObj enemyCode;
    private Animator animator;

    private void Awake()
    {
        enemyCode = gameObject.GetComponent<EnemyPhysObj>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update () {
        if (health <= 0) { GameOver(); }
	}

    public void TakeDamage (int damage)
    {
        health -= damage;
        animator.SetTrigger("Hurt");
        enemyCode.isAttacked = true;
        enemyCode.stopTimer = 0;
        enemyCode.canAttack = false;
    }

    public void GameOver()
    {
        Destroy(gameObject);
    }
}
