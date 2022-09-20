using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Pick_Up_State
{ 
    Default,
    Picked_Up,
    Selected
};

public class Pick_Up : MonoBehaviour
{
    private Inventory inventory;

    public Pick_Up_State pick_Up_State = Pick_Up_State.Default;

    void FixedUpdate()
    {
        if(pick_Up_State == Pick_Up_State.Picked_Up)
        {
            gameObject.SetActive(false);
        }
    }


    public void Set_Function(Inventory inventory)
    {
        this.inventory = inventory;
    }

    public void Press_Button()
    {
        inventory.Button_Press(transform.GetComponent<Pick_Up>());
    }
}
