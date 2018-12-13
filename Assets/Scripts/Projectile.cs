using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public bool enemyProjectile = true;
    public bool fakeProjectile = false;
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
        IDamageable component = (IDamageable)collision.gameObject.GetComponent(typeof(IDamageable));
        if (component != null && !fakeProjectile)
        {
            if (collision.gameObject.GetComponent<PlayerMain>() && enemyProjectile) { collision.gameObject.GetComponent<PlayerMain>().TakeDamage(damage); }
            if (collision.gameObject.GetComponent<EnemyHealth>() && !enemyProjectile) { collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage); }
        }
        Despawn();
    }

    public void Despawn()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        despawn = true;
    }
}
