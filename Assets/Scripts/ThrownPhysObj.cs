using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownPhysObj : PhysicsObject {

    private GameObject player;
    public float speed = 5;

    private void Awake()
    {
        player = GameObject.Find("Player");
        targetVelocity.x = speed;
    }

    protected override void ComputeVelocity()
    {
        targetVelocity.x += velocity.x * 1.1f;
    }
}
