using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public bool enemyProjectile = true;
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player" && enemyProjectile) { collision.gameObject.GetComponent<PlayerMain>().health -= damage; }
        if(collision.gameObject.tag == "Enemy" && !enemyProjectile) { collision.gameObject.GetComponent<EnemyHealth>().health -= damage; }
        Destroy(gameObject);
    }
}
