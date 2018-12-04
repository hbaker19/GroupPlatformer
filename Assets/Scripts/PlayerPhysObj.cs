using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysObj : PhysicsObject {

    public float speed = 5;
    public float jumpTakeOffSpeed = 5;

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        if(Input.GetButtonDown("Jump") && grounded && canJump)
        {
            velocity.y += jumpTakeOffSpeed;
            canJump = false;
        }
        targetVelocity = move * speed;
    }
}
