using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    
    public float projectileSpeed = 5f;
    public GameObject explodeProjectile;

    public void Explode()
    {
        GameObject projectile = (GameObject)Instantiate(explodeProjectile, gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity += Vector2.up * projectileSpeed;
        projectile = (GameObject)Instantiate(explodeProjectile, gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity += new Vector2(1,1).normalized * projectileSpeed;
        projectile = (GameObject)Instantiate(explodeProjectile, gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity += new Vector2(-1,1).normalized * projectileSpeed;
    }
}
