using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pick_Able : MonoBehaviour
{
    [SerializeField] private GameObject scroll_Prefarb;
    [SerializeField] private bool picked_Up;

    private GameObject weapon_Cotainer_Publ;
    private GameObject weapon_Inventory_Publ;

    public void Pick_Up_Or_Drop()
    {
        if(weapon_Cotainer_Publ == null) { return; }

        transform.SetParent(weapon_Cotainer_Publ.transform, false);
        transform.localPosition = new Vector3(0.6f, 0, 0);

        Take_Out_From_Scroll();
        picked_Up = true;

        BoxCollider2D collider = transform.GetComponent<BoxCollider2D>();
        collider.enabled = false;


        scroll_Prefarb.transform.SetParent(weapon_Inventory_Publ.transform, false);
        scroll_Prefarb.SetActive(true);
    }

    public void Show_In_Scroll(GameObject parent_Panel, GameObject weapon_Container, GameObject invetory)
    {
        if (picked_Up == true) { return; }

        scroll_Prefarb.SetActive(true);
        scroll_Prefarb.transform.SetParent(parent_Panel.transform, false);

        RectTransform rect = scroll_Prefarb.GetComponent<RectTransform>();
        rect.localScale = new Vector3(1,1,1);

        weapon_Cotainer_Publ = weapon_Container;
        weapon_Inventory_Publ = invetory;

    }

    public void Take_Out_From_Scroll()
    {
        if (picked_Up == true) { return; }

        scroll_Prefarb.SetActive(false);
        scroll_Prefarb.transform.SetParent(transform, false);

        weapon_Cotainer_Publ = null;
        //weapon_Inventory_Publ = null;
    }
}
