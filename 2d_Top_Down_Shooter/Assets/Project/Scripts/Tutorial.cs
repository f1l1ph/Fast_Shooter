using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject shoot_Text;
    [SerializeField] private GameObject move_Text;

    private void Update()
    {
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
}
