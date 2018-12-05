using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour {

    public bool isAttack = false;
    private float atkTimer = 0f;
    public float atkDuration = 0.5f;
    public int damage = 1;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (gameObject.GetComponent<BoxCollider2D>().enabled == true) { gameObject.GetComponent<BoxCollider2D>().enabled = false; }
        if (isAttack)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer >= atkDuration)
            {
                atkTimer = 0;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                isAttack = false;
            }
        }
        if (Input.GetButtonDown("Fire1") && !isAttack)
        {
            gameObject.GetComponent<PlayerPhysObj>().isStopped = true;
            isAttack = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "Enemy") { collision.gameObject.GetComponent<EnemyHealth>().health -= damage; }
    }
}
