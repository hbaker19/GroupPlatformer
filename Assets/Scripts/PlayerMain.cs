using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerMain : MonoBehaviour, IDamageable {

    public bool isWinter = false;
    public bool isAttack = false;
    private float atkTimer = 0f;
    public float atkDuration = 0.25f;
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
    public float gameTime = 180f;
    private bool gameover = false;
    public float gameoverDuration = 3f;
    private float gameoverTime = 0f;
    public int scoreLossOnDeath = 1000;
    private PlayerPhysObj playerMove;
    public int ammunition = 0;

    public bool invuln = false;
    private float invulnTimer = 0f;
    public float invulnTime = 2f;
    private Animator trayAnimator;

    private Canvas canvas;
    private Slider healthSlider;
    private Text lifeText;
    private Text timeText;
    private Text nuggetText;
    private Canvas pauseCanvas;

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
        playerMove = gameObject.GetComponent<PlayerPhysObj>();
        canvas = gameObject.transform.Find("PlayerCanvas").GetComponent<Canvas>();
        healthSlider = canvas.transform.Find("HealthSlider").GetComponent<Slider>();
        healthSlider.value = health;
        lifeText = canvas.transform.Find("LivesImage/Text").GetComponent<Text>();
        timeText = canvas.transform.Find("TimeImage/Text").GetComponent<Text>();
        nuggetText = canvas.transform.Find("NuggetImage/Text").GetComponent<Text>();
        pauseCanvas = gameObject.transform.Find("PauseCanvas").GetComponent<Canvas>();
    }

    private void Update()
    {
        lifeText.text = "LIVES: " + Persistant.persistant.lives;
        nuggetText.text = "NUGGETS: " + ammunition;
        timeText.text = "" + (int)gameTime;
        time = Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.P)) { TogglePause(); }
        if (Time.timeScale != 0)
        {
            if (gameover)
            {
                playerMove.isStopped = true;
                gameoverTime += time;
                if (gameoverTime >= gameoverDuration)
                {
                    gameover = false;
                    if (Persistant.persistant.lives <= 0) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
                    else { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
                }
            }
            else
            {
                if (invuln)
                {
                    invulnTimer += time;
                    if (invulnTimer >= invulnTime)
                    {
                        invuln = false;
                        invulnTimer = 0f;
                        ChangeColour(defaultColour);
                    }
                    if ((int)(invulnTimer * 10) % 2 == 1) { ChangeColour(defaultColour.x, defaultColour.y, defaultColour.z, defaultColour.w - (defaultColour.w * 0.2f)); }
                    if ((int)(invulnTimer * 10) % 2 == 0) { ChangeColour(defaultColour); }
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
                    playerMove.isStopped = true;
                    isAttack = true;
                    servingTray.SetActive(true);
                    trayAnimator.SetTrigger("Whack");
                }
                if (Input.GetButtonDown("Fire3") && Persistant.persistant.canChange)
                {
                    ChangeSeason();
                }
                shootTimer += time;
                if (Input.GetButtonDown("Fire2") && shootTimer >= shootTime && ammunition > 0)
                {
                    var mousePos = Input.mousePosition;
                    mousePos.z = 10;
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
                    mousePosition.z = 0;
                    GameObject projectile = (GameObject)Instantiate(nuggetProjectile, gameObject.transform.position + (mousePosition - gameObject.transform.position).normalized, Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().velocity += ((Vector2)mousePosition - (Vector2)gameObject.transform.position).normalized * projectileSpeed;
                    shootTimer = 0;
                    ammunition--;
                }
                gameTime -= time;
                if (gameTime <= 0) { GameOver(); }
            }
        }
    }

    public void TogglePause()
    {
        pauseCanvas.GetComponent<Buttons>().Pause();
        if(Time.timeScale != 0) { pauseCanvas.enabled = false; }
        else { pauseCanvas.enabled = true; }
    }
    public void ChangeColour(Vector4 colour)
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color = colour;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
    public void ChangeColour(float r, float g, float b, float a)
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

    public void GameOver()
    {
        if (!gameover)
        {
            Persistant.persistant.lives--;
            Persistant.persistant.score -= scoreLossOnDeath;
            gameObject.GetComponent<Explosion>().Explode();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameover = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invuln)
        {
            invuln = true;
            health--;
            healthSlider.value = health;
            if (health <= 0)
            {
                GameOver();
            }
        }
    }
}
