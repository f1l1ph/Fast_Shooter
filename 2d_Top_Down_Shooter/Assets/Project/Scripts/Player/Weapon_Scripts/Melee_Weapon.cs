using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pick_Up))]
public class Melee_Weapon : MonoBehaviour, IGun
{
    [SerializeField] private int energy_Consumption = 0;
    [SerializeField] private float  hits_Per_Second = 0.5f;

    [SerializeField] private RectTransform ui_element;

    private bool can_Hit = true;
    private float hit_Able_F;

    public int energy_Needed_To_Shoot { get; set; }
    public int inventory_Position { get; set; }
    public RectTransform ui_Element { get; set; }
    public GameObject this_Gameobject { get; set; }

    private void Awake()
    {
        energy_Needed_To_Shoot = energy_Consumption;
        ui_Element = ui_element;
        this_Gameobject = gameObject;
        hit_Able_F = hits_Per_Second;
    }

    private void Update()
    {
        Shoot_Time_Check();
    }

    public bool Shoot(GameObject target)
    {
        if (!can_Hit) { return false; }




        return true;
    }

    private void Shoot_Time_Check()
    {
        if (!can_Hit && hit_Able_F > 0)
        {
            hit_Able_F -= Time.deltaTime;
        }
        else if (hit_Able_F <= 0)
        {
            can_Hit = true;
            hit_Able_F = hits_Per_Second;
        }
    }
}
