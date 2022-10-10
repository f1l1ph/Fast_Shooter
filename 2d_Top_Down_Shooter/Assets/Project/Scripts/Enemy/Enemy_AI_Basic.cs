using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(IAttack_Able))]
public class Enemy_AI_Basic : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Header("Moving")]
    [Tooltip("Speed when rotating towards player")] [SerializeField] private float rotation_Speed   = 5;
    [Tooltip("Moving speed")]                       [SerializeField] private float speed            = 200f;
    [SerializeField] private float next_Waypoint_Distance = 3;

    [Header("Attacking")]
    [SerializeField] private float distance_To_Attack = 5f;
    bool attacking = false;

    [Header("Waypoints")]
    [SerializeField] private Transform[] waypoints;
    bool isWaypointing = true;
    private int current_Waypoint_In_Array = 0;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    [Tooltip("Graphics that will be rotated towards the target")]
    [SerializeField] private Transform graphicks;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0, 0.5f);
    }

    void FixedUpdate()
    {
        CalculatePathAndMove();

        if (isWaypointing)
        {
            Rotate_Towards_Point();
            FollowWayPoints();
        }
        else if (!attacking)
        {
            Rotate_Towards_Point();
        }
        else
        {
            Rotate_Towards_Target();
        }

        if (!isWaypointing)
        {
            Attack();
        }
    }

    private void CalculatePathAndMove()
    {
        if (path == null) { return; }

        if (currentWaypoint + 1 >= path.vectorPath.Count) 
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < next_Waypoint_Distance)
        {
            currentWaypoint++;
        }
    }

    private void Rotate_Towards_Point()
    {
        Vector2 v = rb.velocity;

        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        graphicks.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Rotate_Towards_Target()
    {
        Vector3 vector_To_Target = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vector_To_Target.y, vector_To_Target.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotation_Speed);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void FollowWayPoints()
    {
        //isWaypointing = true;
        //attacking = false;
        
        target = waypoints[current_Waypoint_In_Array];
        float distance_To_Target = Vector2.Distance(target.transform.position, transform.position);

        if (distance_To_Target <= 2) 
        {
            current_Waypoint_In_Array++;
            if(current_Waypoint_In_Array >= waypoints.Length)
            {
                current_Waypoint_In_Array = 0;
            }
        }
    }

    private void Attack()
    {
        float distance_Between_Target = Vector2.Distance(target.position, transform.position);

        if(distance_Between_Target <= distance_To_Attack)
        {
            //attack
            attacking = true;
            GetComponent<IAttack_Able>().Attack(target.gameObject);
        }
        else
        {
            //nemam vzdialenost na utok
            attacking = false;
            return;
        }
    }

    public void SetTargetAndAttack(Transform target)
    {
        isWaypointing   = false;
        this.target     = target;
        Attack();
    }
}
