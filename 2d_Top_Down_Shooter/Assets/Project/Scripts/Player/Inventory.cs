using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Tooltip("Ui container for inventory objects.")]
    [SerializeField] private GameObject inventory_Graphick;

    [SerializeField] private IGun[] invetory = { null, null, null };
    [SerializeField] private int selected_Gun = 0;

    [Tooltip("This will be a UI conatiner of guns that are on the ground.")]
    [SerializeField] private GameObject panel;

    [Tooltip("This is gameobject container of guns.")]
    [SerializeField] private GameObject gun_Container;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<IGun>() != null)
        {
            if (other.GetComponent<Pick_Up>().pick_Up_State == Pick_Up_State.Default)
            {
                Show_In_Panel(other.GetComponent<IGun>());

                Pick_Up pick_Up = other.GetComponent<Pick_Up>();
                pick_Up.Set_Function(this);
            }
        }
    }

    void Add_To_Inventory(GameObject weapon)
    {
        //set position parent and scale
        weapon.transform.SetParent(gun_Container.transform, false);
        weapon.transform.localPosition = new Vector3(0.6f, 0);

        IGun gun = weapon.GetComponent<IGun>();

        gun.ui_Element.transform.SetParent(inventory_Graphick.transform, false);
        gun.ui_Element.transform.localScale = Vector3.one;

        weapon.transform.GetComponent<Collider2D>().enabled = false;


        // add weapon to inventory
        for (int i = 0; i < invetory.Length; i++)
        {
            if (invetory[i] == null)
            {
                invetory[i] = weapon.GetComponent<IGun>();
                weapon.GetComponent<IGun>().inventory_Position = i;
                return;
            }
            else if (invetory[i] != null && i == invetory.Length)
            {
                invetory[selected_Gun] = weapon.GetComponent<IGun>();
                weapon.GetComponent<IGun>().inventory_Position = selected_Gun;
            }
        }
    }

    void Take_From_Inventory()
    {
        
    }

    void Select_Gun(GameObject gun)
    {
        for (int i = 0; i < invetory.Length; i++) 
        {
            if (invetory[i] != null)
            {
                Pick_Up pick_Up = invetory[i].this_Gameobject.GetComponent<Pick_Up>();
                pick_Up.pick_Up_State = Pick_Up_State.Picked_Up;
                pick_Up.gameObject.SetActive(false);
            }
        }
        gun.gameObject.SetActive(true);

        IGun gun_Igun = gun.GetComponent<IGun>();
        selected_Gun = gun_Igun.inventory_Position;

        gun.transform.GetComponent<Pick_Up>().pick_Up_State = Pick_Up_State.Selected;
        gun.gameObject.SetActive(true);
    }

    public void Button_Press(Pick_Up pick_Up)
    {
        if (pick_Up.pick_Up_State == Pick_Up_State.Picked_Up)//when selecting
        {
            Debug.Log("when selecting");
            Select_Gun(pick_Up.gameObject);
        }

        if (pick_Up.pick_Up_State == Pick_Up_State.Default)//when picking up
        {
            Debug.Log("when picking up");
            pick_Up.pick_Up_State = Pick_Up_State.Picked_Up;

            Add_To_Inventory(pick_Up.gameObject);
        }
    }

    private void Show_In_Panel(IGun gun)
    {
        gun.ui_Element.transform.SetParent(panel.transform);

        gun.ui_Element.transform.localScale = Vector3.one;
        
    }

    public void Shoot(GameObject aim)
    {
        if (invetory[selected_Gun] != null) 
        {
            invetory[selected_Gun].Shoot(aim);
        }
    }
}
