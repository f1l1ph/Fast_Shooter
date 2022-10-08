using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(IAttack_Able))]
public class Enemy_AI_Basic : MonoBehaviour
{
    [SerializeField] private float rotation_Speed = 5;

    [SerializeField] private Transform target;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float next_Waypoint_Distance = 3;

    bool attacking = false;
    [SerializeField] private float distance_To_Attack = 5f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;// if true than attack

    Seeker seeker;
    Rigidbody2D rb;

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

        if (!attacking)
        {
            Rotate_Towards_Point();
        }
        else
        {
            Rotate_Towards_Target();
        }

        Attack();
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
}
