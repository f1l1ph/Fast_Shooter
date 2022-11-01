using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(IAttack_Able))]
public class Enemy_AI_Basic : MonoBehaviour
{
    private Transform target;

    [Header("Moving")]
    [Tooltip("Speed when rotating towards player")] [SerializeField] private float rotation_Speed   = 5;
    [Tooltip("Moving speed")]                       [SerializeField] private float speed            = 200f;
    [SerializeField] private float next_Waypoint_Distance_Inspector = 3;
    private float next_Waypoint_Distance;

    [Tooltip("Will change enemy mass and linear drag when attacking so it can get closer to target, only enable for melle enemies")] 
    [SerializeField] private bool  is_Melee;

    [Header("Attacking")]
    [SerializeField] private float distance_To_Attack = 5f;
    private bool        attacking = false;

    [Header("Waypoints")]
    [SerializeField] private Transform[] waypoints;
    private bool        isWaypointing = true;
    private int         current_Waypoint_In_Array = 0;

    private Path        path;
    private int         currentWaypoint = 0;

    private Seeker      seeker;
    private Rigidbody2D rb;

    [Tooltip("Graphics that will be rotated towards the target")]
    [SerializeField] private Transform graphicks;

    void Start()
    {
        next_Waypoint_Distance = next_Waypoint_Distance_Inspector;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0, 0.5f);
    }

    void FixedUpdate()
    {
        CalculatePathAndMove();

        if (isWaypointing)
        {
           FollowWayPoints();
           next_Waypoint_Distance = next_Waypoint_Distance_Inspector;
        }
        else if (!isWaypointing)
        {
            Attack();
        }

        if (!attacking)
        {
            Rotate_Towards_Velocity();
        }
        else
        {
            Rotate_Towards_Target();
        }
    }

    private void CalculatePathAndMove()
    {
        if (path == null) { return; }

        if (currentWaypoint + 1 >= path.vectorPath.Count) 
        {
            return;
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

    private void Rotate_Towards_Velocity()
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
        graphicks.transform.rotation = Quaternion.Slerp(graphicks.transform.rotation, q, Time.deltaTime * rotation_Speed);
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
        if(target == null) { return; }

        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void FollowWayPoints()
    {
        if (waypoints[0] == null) { return; }
        
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
        next_Waypoint_Distance = 1;
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
