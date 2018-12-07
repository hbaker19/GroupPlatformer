using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;
    public float inertiaFalloff = 5f;
    public float resistance = 0.1f;
    public float inertia = 0;
    public Vector2 targetVelocity;
    
    protected bool canJump = false;
    protected Vector2 velocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb;
    protected float inertiaCalc;
    protected float inertiaMod;
    protected float inertiaCalcX;
    protected float inertiaCalcY;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.05f;

    protected void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Start()
    {
        contactFilter.useTriggers = false;
        LayerMask ignoreLayer = ~(1 << LayerMask.NameToLayer("Projectile"));
        contactFilter.SetLayerMask(ignoreLayer & Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    protected void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity() { }

    protected void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;
        velocity.y += targetVelocity.y;
        inertiaCalc = Time.deltaTime * inertiaFalloff;
        inertiaCalcX = (velocity.x / inertiaFalloff) - inertia * 0.1f;
        inertiaMod = Mathf.Abs(inertia) > 10 ? 10 * Mathf.Sign(inertia) : inertia;
        if (inertiaMod != 0)
        {
            if (inertiaMod > 0)
            {
                if (inertiaMod - (inertiaCalc - (inertiaCalcX < 0 ? inertiaCalcX : 0)) < 0)
                {
                    inertiaMod = 0;
                }
                else
                {
                    inertiaMod -= (inertiaCalc - (inertiaCalcX < 0 ? inertiaCalcX : 0));
                }
            }
            if (inertiaMod < 0)
            {
                if (inertiaMod + inertiaCalc + (inertiaCalcX > 0 ? inertiaCalcX : 0) > 0)
                {
                    inertiaMod = 0;
                }
                else
                {
                    inertiaMod += inertiaCalc + (inertiaCalcX > 0 ? inertiaCalcX : 0);
                }
            }
        }
        velocity.x += inertia;
        inertia = inertiaMod;
        if(Mathf.Abs(velocity.x) > resistance) { velocity.x += velocity.x > 0 ? -resistance : resistance; }

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);
        move = Vector2.up * deltaPosition.y;
        Movement(move, true);
    }

    protected void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
        if(distance > minMoveDistance)
        {
            int count = rb.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for(int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }
            for(int i = 0; i < hitBufferList.Count; i++)
            {
                if(hitBufferList[i].collider.GetComponent<PhysicsObject>() != null)
                {
                    hitBufferList[i].collider.GetComponent<PhysicsObject>().inertia += velocity.x * resistance;
                }
                Vector2 currentNormal = hitBufferList[i].normal;
                Debug.DrawRay(transform.position, move.normalized, Color.blue);
                Debug.DrawRay(hitBufferList[i].transform.position, currentNormal, Color.green);
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if(move.y < 0) { canJump = true; }
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(velocity, currentNormal);
                if(projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }
                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        rb.position = rb.position + move.normalized * distance;
    }
}
