using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterEnemy : EnemyPhysObj {

    private RaycastHit2D[] playerHitBuffer = new RaycastHit2D[1];
    private float timer = 0f;
    public float atkDelay = 2f;
    private float atkTimer = 0f;
    private bool isAttack = false;
    public float atkDuration = 0.25f;
    public float atkRange = 2;
    private GameObject bat;
    private Animator batAnimator;
    private Vector3 batPosition;

    protected override void Awake()
    {
        base.Awake();
        bat = transform.Find("BrusselBat").gameObject;
        batAnimator = bat.GetComponent<Animator>();
        batPosition = bat.transform.localPosition;
        if(wallCheckDistance >= atkRange) { wallCheckDistance = atkRange - 0.1f; }
    }

    protected override void Action()
    {
        bat.transform.localPosition = batPosition;
        Physics2D.Raycast(gameObject.transform.position, Vector2.right * direction, contactFilter, playerHitBuffer, atkRange);
        if (playerHitBuffer[0].collider != null)
        {
            if (playerHitBuffer[0].collider.gameObject.name == "Player")
            {
                isStopped = true;
                if (isAttack)
                {
                    atkTimer += Time.deltaTime;
                    if (atkTimer >= atkDuration)
                    {
                        bat.GetComponent<BoxCollider2D>().enabled = false;
                        atkTimer = 0;
                        isAttack = false;
                    }
                }
                else { timer += Time.deltaTime; }
                if (timer >= atkDelay)
                {
                    bat.GetComponent<BoxCollider2D>().enabled = true;
                    batAnimator.SetTrigger("Whack");
                    isAttack = true;
                    timer = 0;
                }
            }
            else { timer = 0; }
        }
        System.Array.Clear(playerHitBuffer, 0, playerHitBuffer.Length);
    }
}
