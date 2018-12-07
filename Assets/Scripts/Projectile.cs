using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public bool enemyProjectile = true;
    public int damage = 1;
    private float timer = 0f;
    private bool despawn = false;
    public float despawnTime = 1f;

    private void Update()
    {
        if (despawn)
        {
            timer += Time.deltaTime;
            if (timer >= despawnTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player" && enemyProjectile) { collision.gameObject.GetComponent<PlayerMain>().TakeDamage(damage); }
        if(collision.gameObject.tag == "Enemy" && !enemyProjectile) { collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage); }
        gameObject.GetComponent<Collider2D>().enabled = false;
        despawn = true;
    }
}
