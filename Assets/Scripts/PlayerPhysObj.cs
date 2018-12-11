using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysObj : PhysicsObject {

    public float speed = 5;
    public float climbSpeed = 3;
    public float jumpTakeOffSpeed = 5;
    public bool isStopped = false;
    public float direction = 1;

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        canClimb = true;
    }

    protected override void ComputeVelocity()
    {
        if (!isStopped)
        {
            Vector2 move = Vector2.zero;
            move.x = Input.GetAxis("Horizontal");
            if(move.x > 0) { animator.SetBool("FaceLeft", false); }
            else if(move.x < 0) { animator.SetBool("FaceLeft", true); }
            move.y = Input.GetAxis("Vertical");
            if (Input.GetButtonDown("Jump") && grounded && canJump)
            {
                velocity.y += jumpTakeOffSpeed;
                canJump = false;
                isJumping = true;
            }
            if(Input.GetButtonUp("Jump") && isJumping)
            {
                velocity.y = velocity.y / 2;
            }
            if(Mathf.Sign(move.x) != Mathf.Sign(direction) && move.x != 0) { direction = -direction; gameObject.GetComponent<BoxCollider2D>().offset = gameObject.GetComponent<BoxCollider2D>().offset * - 1; }
            targetVelocity.x = move.x * speed;
            overrideVelocity.y = move.y * climbSpeed;
        }
        else
        {
            if (!gameObject.GetComponent<PlayerMain>().isAttack) { isStopped = false; }
        }
    }
}
