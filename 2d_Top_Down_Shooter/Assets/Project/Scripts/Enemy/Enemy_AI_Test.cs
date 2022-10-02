using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_AI_Test : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float next_Waypoint_Distance = 3f;

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
        if (path == null) { return; }

        CalculatePathAndMove();
        Rotate_Towards_Point();
    }

    private void CalculatePathAndMove()
    {
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
        //if (!reachedEndOfPath)
        //{
            Vector2 v = rb.velocity;
            
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            graphicks.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //}
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
}
