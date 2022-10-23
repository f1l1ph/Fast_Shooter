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
    [SerializeField] private GameObject UI_Element;

    private Inventory inventory;

    public Pick_Up_State pick_Up_State = Pick_Up_State.Default;

    void FixedUpdate()
    {
        if (pick_Up_State == Pick_Up_State.Picked_Up)
        {
            gameObject.SetActive(false);
        }

        Check_Selection();
    }

    public void Check_Selection()
    {
        if (pick_Up_State == Pick_Up_State.Selected)
        {
            UI_Element.transform.localScale = Vector3.one * 1.25f;
        }
        else
        {
            UI_Element.transform.localScale = Vector3.one;
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
