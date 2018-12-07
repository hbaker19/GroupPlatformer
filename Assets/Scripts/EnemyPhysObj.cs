using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysObj : PhysicsObject {

    public bool paces = false;
    public float paceDistance = 3f;
    private bool home = true;
    private Vector2 homePosition;

    public bool canAttack = true;

    public float speed = 3;
    private Vector2 fallCheck = new Vector2(0.25f, -1f);
    private RaycastHit2D[] fallBuffer = new RaycastHit2D[16];
    protected int direction = -1;
    public bool isStopped = false;
    public float stopTimer = 0f;
    public float stopTime = 1f;

    protected virtual void Awake()
    {
        if (paces) { homePosition = gameObject.transform.position; }
    }

    protected override void ComputeVelocity()
    {
        if (Mathf.Sign(direction) != Mathf.Sign(fallCheck.x)) { fallCheck.x = -fallCheck.x; }
        Physics2D.Raycast(gameObject.transform.position, fallCheck, contactFilter, fallBuffer, 1);
        if (IsEmpty(fallBuffer)) { direction = -direction; isStopped = true; }
        System.Array.Clear(fallBuffer, 0, fallBuffer.Length);
        if (paces)
        {
            if (Mathf.Abs(gameObject.transform.position.x - homePosition.x) > paceDistance && home) { home = false; direction = -direction; isStopped = true; }
            else if (Mathf.Abs(gameObject.transform.position.x - homePosition.x) < paceDistance && !home) { home = true; }
        }
        Action();
        if (isStopped)
        {
            targetVelocity.x = 0;
            stopTimer += Time.deltaTime;
            if(stopTimer >= stopTime) { isStopped = false; stopTimer = 0; canAttack = true; }
        }
        else
        {
            targetVelocity.x = speed * direction;
        }
    }

    protected virtual void Action() { }

    private bool IsEmpty (RaycastHit2D[] arrayToCheck)
    {
        for(int i = 0; i < arrayToCheck.Length; i++)
        {
            if (arrayToCheck[i].collider != null)
            {
                return false;
            }
        }
        return true;
    }
}
