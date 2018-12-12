using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whack : MonoBehaviour {

    public bool enemyAttack = true;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && enemyAttack) { collision.gameObject.GetComponent<PlayerMain>().TakeDamage(damage); }
        if (collision.gameObject.tag == "Enemy" && !enemyAttack) { collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage); }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile") && ((collision.gameObject.GetComponent<Projectile>().enemyProjectile && !enemyAttack) || (!collision.gameObject.GetComponent<Projectile>().enemyProjectile && enemyAttack))) { Destroy(collision.gameObject); }
    }
}
