using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    [Header("Texts to activate")]
    [SerializeField] private GameObject shoot_Text;
    [SerializeField] private GameObject move_Text;
    [SerializeField] private GameObject pick_Gun_Text;
    [SerializeField] private GameObject complete_Level_Text;

    [Header("Joysticks")]
    [SerializeField] private GameObject joyStick_Left;
    [SerializeField] private GameObject joyStick_Right;

    [SerializeField] private Player_Movement _Movement;

    [SerializeField] private Pick_Up pick_Up;

    [Header("Shooting enemy")]
    [SerializeField] private float rotation_Speed = 1f;
    [SerializeField] private GameObject arrow_Towards_Enemy;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemy_Shoot_Text;

    private bool shooting = false;
    private bool moveing = false;

    private void Start()
    {
        _Movement.enabled = false;
        move_Text.SetActive(false);
        shoot_Text.SetActive(false);

        joyStick_Left.SetActive(false);
        joyStick_Right.SetActive(false);

        arrow_Towards_Enemy.SetActive(false);
        enemy_Shoot_Text.SetActive(false);

        complete_Level_Text.SetActive(false);
    }

    private void Update()
    {
        if(moveing && shooting) { Rotate_Towards_Enemy_And_Set_Text(); }

        if (!shooting) { return; }

        _Movement.enabled = true;

        Gamepad gamepad = Gamepad.current;
        Vector2 move = gamepad.leftStick.ReadValue();
        Vector2 look = gamepad.rightStick.ReadValue();

        if (move != Vector2.zero)
        {
            move_Text.SetActive(false);
            moveing = true;
        }
        if (look != Vector2.zero)
        {
            shoot_Text.SetActive(false);
            Can_Move();
        }
    }

    public void Pick_Up_Gun()
    {
        pick_Gun_Text.SetActive(false);
       
        shoot_Text.SetActive(true);
        
        joyStick_Right.SetActive(true);

        pick_Up.Press_Button();
       
        shooting = true;
    }

    private void Can_Move()
    {
        joyStick_Left.SetActive(true);
        if (!moveing)
        {
            move_Text.SetActive(true);
        }
    }

    private void Rotate_Towards_Enemy_And_Set_Text()
    {
        if(enemy == null) 
        { 
            arrow_Towards_Enemy.SetActive(false);
            enemy_Shoot_Text.SetActive(false);

            complete_Level_Text.SetActive(true);
            return;
        }
        arrow_Towards_Enemy.SetActive(true);
        enemy_Shoot_Text.SetActive(true);

        Vector3 vector_To_Target = enemy.transform.position - arrow_Towards_Enemy.transform.position;
        float angle = Mathf.Atan2(vector_To_Target.y, vector_To_Target.x) * Mathf.Rad2Deg;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        arrow_Towards_Enemy.transform.rotation = Quaternion.Slerp(arrow_Towards_Enemy.transform.rotation, q, Time.deltaTime * rotation_Speed);
    }
}
