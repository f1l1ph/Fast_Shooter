using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject aim;

    [Header("Moving")]
    [SerializeField] private float          move_Speed = 5f;
    [SerializeField] private float          max_Move_Velocity;

    [Header("Rotating")]
    [SerializeField] private float          rotation_Speed;
    [SerializeField] private float          aim_Distance_Multiplayer;

    [Tooltip("this will also be a weapon container")]
    [SerializeField] private Transform      graphicks;

    [Tooltip("This will be also a conatiner of guns that are on the ground")]
    [SerializeField] private GameObject panel;

    [SerializeField] private GameObject invetory;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //gather input values
        Gamepad gamepad = Gamepad.current;
        Vector2 move = gamepad.leftStick.ReadValue();
        Vector2 look = gamepad.rightStick.ReadValue();

        Move_Player(move);
        Aim_Player(look);
    }

    private void Aim_Player(Vector2 look)
    {
        if (look.x != 0 || look.x != 0)
        {
            //Shoot
            IGun gun2 = graphicks.GetComponentInChildren<IGun>();
            if (gun2 != null)
            {
                gun2.Shoot(aim);
            }
            

            aim.transform.localPosition = new Vector3(
                look.x * Time.deltaTime * aim_Distance_Multiplayer,
                look.y * Time.deltaTime * aim_Distance_Multiplayer,
                0);
        }

        if (aim.transform.position != transform.position)
        {
            Vector3 vector_To_Target = aim.transform.position - graphicks.transform.position;
            float angle = Mathf.Atan2(vector_To_Target.y, vector_To_Target.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            graphicks.transform.rotation = Quaternion.Slerp(graphicks.transform.rotation, q, Time.deltaTime * rotation_Speed);
        }
    }

    private void Move_Player(Vector2 move)
    {
        rb.AddForce(move * move_Speed);
        Vector2 rb_Max_Velocity = Vector2.ClampMagnitude(rb.velocity, max_Move_Velocity);
        rb.velocity = rb_Max_Velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Pick_Able>() != null)
        {
            Pick_Able pick_Able = collision.GetComponent<Pick_Able>();
            pick_Able.Show_In_Scroll(panel, graphicks.gameObject, invetory);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Pick_Able>() != null)
        {
            Pick_Able pick_Able = collision.GetComponent<Pick_Able>();
            pick_Able.Take_Out_From_Scroll();
        }
    }
}
