using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject shoot_Text;
    [SerializeField] private GameObject move_Text;
    [SerializeField] private GameObject pick_Gun_Text;

    [SerializeField] private GameObject joyStick_Left;
    [SerializeField] private GameObject joyStick_Right;

    [SerializeField] private Player_Movement _Movement;

    [SerializeField] private Pick_Up pick_Up;

    private bool shooting = false;

    private void Start()
    {
        _Movement.enabled = false;
        move_Text.SetActive(false);
        shoot_Text.SetActive(false);

        joyStick_Left.SetActive(false);
        joyStick_Right.SetActive(false);
    }

    private void Update()
    {
        if (!shooting) { return; }

        _Movement.enabled = true;

        Gamepad gamepad = Gamepad.current;
        Vector2 move = gamepad.leftStick.ReadValue();
        Vector2 look = gamepad.rightStick.ReadValue();

        if (move != Vector2.zero)
        {
            move_Text.SetActive(false);
        }
        if (look != Vector2.zero)
        {
            shoot_Text.SetActive(false);
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
}
