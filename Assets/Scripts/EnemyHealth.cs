using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {

    public int health = 1;
    private EnemyPhysObj enemyCode;
    private Animator animator;
    public int scoreValue = 100;
    public bool givesAmmo = false;
    public int ammoDrop = 3;

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
        if (gameObject.GetComponent<Explosion>()) { gameObject.GetComponent<Explosion>().Explode(); }
        if (givesAmmo) { Persistant.persistant.ammunition += ammoDrop; }
        Persistant.persistant.score += scoreValue;
        Destroy(gameObject);
    }
}
