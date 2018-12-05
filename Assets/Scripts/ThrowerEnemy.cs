using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerEnemy : EnemyPhysObj {

    private GameObject player;
    private RaycastHit2D[] playerHitBuffer = new RaycastHit2D[1];
    private float timer = 0f;
    public float timerMax = 2f;
    public float viewDistance = 5f;
    public float projectileSpeed = 5f;
    public GameObject enemyProjectile;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player");
    }

    protected override void Action()
    {
        Physics2D.Raycast(gameObject.transform.position, player.transform.position - gameObject.transform.position, contactFilter, playerHitBuffer, viewDistance);
        if (playerHitBuffer[0].collider != null)
        {
            if (playerHitBuffer[0].collider.gameObject.name == "Player")
            {
                isStopped = true;
                timer += Time.deltaTime;
                if (timer >= timerMax)
                {
                    timer = 0;
                    GameObject projectile = (GameObject)Instantiate(enemyProjectile, gameObject.transform.position + (player.transform.position - gameObject.transform.position).normalized, Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().velocity += ((Vector2)player.transform.position - (Vector2)gameObject.transform.position + Vector2.up).normalized * projectileSpeed;
                }
            }
            else
            {
                timer = 0;
            }
        }
        System.Array.Clear(playerHitBuffer, 0, playerHitBuffer.Length);
    }
}

//GameObject projectile = (GameObject)Instantiate(enemyProjectile, enemyPos + (playerDir), Quaternion.Euler(0f, 0f, (Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg) + 90));
//projectile.GetComponent<Rigidbody2D>().velocity = playerDir* projectileSpeed;