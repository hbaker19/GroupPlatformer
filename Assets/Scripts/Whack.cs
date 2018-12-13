using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whack : MonoBehaviour {

    public bool enemyAttack = true;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable component = (IDamageable)collision.gameObject.GetComponent(typeof(IDamageable));
        if (component != null)
        {
            if (collision.gameObject.GetComponent<PlayerMain>() && enemyAttack) { collision.gameObject.GetComponent<PlayerMain>().TakeDamage(damage); }
            if (collision.gameObject.GetComponent<EnemyHealth>() && !enemyAttack) { collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage); }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile") && ((collision.gameObject.GetComponent<Projectile>().enemyProjectile && !enemyAttack) || (!collision.gameObject.GetComponent<Projectile>().enemyProjectile && enemyAttack))) { Destroy(collision.gameObject); }
    }
}
