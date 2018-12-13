using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysObj : PhysicsObject {

    public bool moves = true;
    public bool paces = false;
    public float paceDistance = 3f;
    private bool home = true;
    private Vector2 homePosition;
    protected Animator animator;

    public bool canAttack = true;

    public float speed = 3;
    public float fallCheckDistance = 1;
    private Vector2 fallCheck = new Vector2(0.25f, -1f);
    private RaycastHit2D[] fallBuffer = new RaycastHit2D[16];
    public float wallCheckDistance = 2;
    private RaycastHit2D[] wallBuffer = new RaycastHit2D[1];
    protected int direction = -1;
    public bool isStopped = false;
    public bool isAttacked = false;
    public float stopTimer = 0f;
    public float stopTime = 1f;
    public float hitStopTimer = 2f;

    protected virtual void Awake()
    {
        if (paces) { homePosition = gameObject.transform.position; }
        animator = gameObject.GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Action();
        if (moves)
        {
            if (Mathf.Sign(direction) != Mathf.Sign(fallCheck.x)) { fallCheck.x = -fallCheck.x; }
            Physics2D.Raycast(gameObject.transform.position, fallCheck, contactFilter, fallBuffer, fallCheckDistance);
            Debug.DrawRay(gameObject.transform.position, fallCheck, Color.cyan);
            if (IsEmpty(fallBuffer)) { direction = -direction; isStopped = true; transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); }
            Physics2D.Raycast(gameObject.transform.position, Vector2.right * direction, contactFilter, wallBuffer, wallCheckDistance);
            Debug.DrawRay(gameObject.transform.position, Vector2.right * direction, Color.magenta);
            if (wallBuffer[0].collider != null) { if (wallBuffer[0].collider.tag != "Player") { direction = -direction; isStopped = true; transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); } }
            System.Array.Clear(fallBuffer, 0, fallBuffer.Length);
            System.Array.Clear(wallBuffer, 0, wallBuffer.Length);
            if (paces)
            {
                if (Mathf.Abs(gameObject.transform.position.x - homePosition.x) > paceDistance && home) { home = false; direction = -direction; isStopped = true; transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); }
                else if (Mathf.Abs(gameObject.transform.position.x - homePosition.x) < paceDistance && !home) { home = true; }
            }
            if (isStopped || isAttacked)
            {
                targetVelocity.x = 0;
                stopTimer += Time.deltaTime;
                if (stopTimer >= (isAttacked ? hitStopTimer : stopTime)) { isStopped = false; isAttacked = false; stopTimer = 0; canAttack = true; }
            }
            else
            {
                targetVelocity.x = speed * direction;
            }
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
