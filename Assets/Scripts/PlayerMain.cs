using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerMain : MonoBehaviour, IDamageable {

    public bool isWinter = false;
    public bool isAttack = false;
    private float atkTimer = 0f;
    public float atkDuration = 0.25f;
    public int damage = 1;
    public int health = 3;
    private GameObject winter;
    private GameObject spring;
    private float shootTimer = 0f;
    public float shootTime = 1f;
    public float projectileSpeed = 5f;
    public GameObject nuggetProjectile;
    private float time;
    public Vector4 defaultColour;
    private GameObject servingTray;

    public bool invuln = false;
    private float invulnTimer = 0f;
    public float invulnTime = 2f;
    //private Animator animator;
    private Animator trayAnimator;

    private void Awake()
    {
        //animator = gameObject.GetComponent<Animator>();
        winter = GameObject.Find("Winter");
        spring = GameObject.Find("Spring");
        ChangeSeason();
        servingTray = transform.Find("ServingTray").gameObject;
        trayAnimator = servingTray.GetComponent<Animator>();
        trayAnimator.speed = atkDuration / 0.333f;
        defaultColour = new Vector4(1, 1, 1, 1);
    }

    private void Update()
    {
        time = Time.deltaTime;
        if (invuln)
        {
            invulnTimer += time;
            if(invulnTimer >= invulnTime)
            {
                invuln = false;
                invulnTimer = 0f;
                ChangeColour(defaultColour);
            }
            if((int)(invulnTimer * 10) % 2 == 1) { ChangeColour(defaultColour.x, defaultColour.y, defaultColour.z, defaultColour.w - 0.2f); }
            if((int)(invulnTimer * 10) % 2 == 0) { ChangeColour(defaultColour); }
        }
        if (isAttack)
        {
            atkTimer += time;
            servingTray.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 90 * gameObject.GetComponent<PlayerPhysObj>().direction), Quaternion.Euler(0, 0, 0), atkTimer / atkDuration);
            servingTray.transform.localPosition = Vector3.Slerp(new Vector3(0.05f * gameObject.GetComponent<PlayerPhysObj>().direction, 0, 0), new Vector3(0.1f * gameObject.GetComponent<PlayerPhysObj>().direction, -0.05f, 0), atkTimer / atkDuration);
            if (atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttack = false;
                servingTray.SetActive(false);
            }
        }
        if (Input.GetButtonDown("Fire1") && !isAttack)
        {
            gameObject.GetComponent<PlayerPhysObj>().isStopped = true;
            isAttack = true;
            servingTray.SetActive(true);
            trayAnimator.SetTrigger("Whack");
        }
        if (Input.GetButtonDown("Fire3") && Persistant.persistant.canChange)
        {
            ChangeSeason();
        }
        shootTimer += time;
        if(Input.GetButtonDown("Fire2") && shootTimer >= shootTime && Persistant.persistant.ammunition > 0)
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
            mousePosition.z = 0;
            GameObject projectile = (GameObject)Instantiate(nuggetProjectile, gameObject.transform.position + (mousePosition - gameObject.transform.position).normalized, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity += ((Vector2)mousePosition - (Vector2)gameObject.transform.position).normalized * projectileSpeed;
            shootTimer = 0;
            Persistant.persistant.ammunition--;
        }
    }

    private void ChangeColour(Vector4 colour)
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color = colour;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
    private void ChangeColour(float r, float g, float b, float a)
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.r = r;
        color.b = b;
        color.g = g;
        color.a = a;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
    private void ChangeSeason()
    {
        if (isWinter)
        {
            foreach (Transform child in spring.transform)
            {
                Collider2D childCollider = child.GetComponent<Collider2D>();
                if (childCollider) { childCollider.isTrigger = false; }
                child.GetComponent<TilemapRenderer>().enabled = true;
            }
            foreach (Transform child in winter.transform)
            {
                Collider2D childCollider = child.GetComponent<Collider2D>();
                if (childCollider) { childCollider.isTrigger = true; }
                child.GetComponent<TilemapRenderer>().enabled = false;
            }
            isWinter = false;
        }
        else
        {
            foreach (Transform child in winter.transform)
            {
                Collider2D childCollider = child.GetComponent<Collider2D>();
                if (childCollider) { childCollider.isTrigger = false; }
                child.GetComponent<TilemapRenderer>().enabled = true;
            }
            foreach (Transform child in spring.transform)
            {
                Collider2D childCollider = child.GetComponent<Collider2D>();
                if (childCollider) { childCollider.isTrigger = true; }
                child.GetComponent<TilemapRenderer>().enabled = false;
            }
            isWinter = true;
        }
    }

    public void GameOver() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void TakeDamage(int damage) { if (!invuln) { invuln = true; health--; if (health <= 0) { GameOver(); } } }
}
