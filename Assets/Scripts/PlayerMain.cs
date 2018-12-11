using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour, IDamageable {

    public bool isWinter = false;
    public bool isAttack = false;
    private float atkTimer = 0f;
    public float atkDuration = 0.5f;
    public int damage = 1;
    public int health = 3;
    private float hitGuarantee = 0.1f;
    private float guaranteeTimer = 0f;
    private GameObject winter;
    private GameObject spring;
    //private Animator animator;

    private void Awake()
    {
        //animator = gameObject.GetComponent<Animator>();
        winter = GameObject.Find("Winter");
        spring = GameObject.Find("Spring");
        winter.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.GetComponent<BoxCollider2D>().enabled == true)
        {
            guaranteeTimer += Time.deltaTime;
            if (guaranteeTimer >= hitGuarantee) { gameObject.GetComponent<BoxCollider2D>().enabled = false; }
        }
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
        if (Input.GetButtonDown("Fire2"))
        {
            if (isWinter)
            {
                isWinter = false;
                winter.SetActive(true);
                spring.SetActive(false);
            }
            else
            {
                isWinter = true;
                spring.SetActive(true);
                winter.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "Enemy") { collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage); }
    }

    public void GameOver() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void TakeDamage(int damage) { }
}
