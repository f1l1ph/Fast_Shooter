using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pick_Up))]
public class Melee_Weapon : MonoBehaviour, IGun
{
    [SerializeField] private int energy_Consumption = 0;
    [SerializeField] private float  hits_Per_Second = 0.5f;
    [SerializeField] private Transform point;
    [SerializeField] private Transform point_To_Rotate_Around;
    [SerializeField] private float rotation_F = 1;
    [SerializeField] private float damage = 30f;
    [SerializeField] private float force_To_Give = 1f;

    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject graphicks;
    [SerializeField] private Melee_Graphicks graphicks_Script;

    [SerializeField] private RectTransform ui_element;

    private bool can_Hit = true;
    private float hit_Able_F;
    bool attacking = false;

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

        graphicks_Script.damage = damage;
        graphicks_Script.force_To_Hit = force_To_Give;
    }

    private void Update()
    {
        Shoot_Time_Check();

        Pick_Up pick = transform.GetComponent<Pick_Up>();
        if (pick.pick_Up_State == Pick_Up_State.Selected || pick.pick_Up_State == Pick_Up_State.Picked_Up)
        {
            if(attacking) { return; }
            graphicks.transform.position = point.transform.position;
            graphicks.transform.localRotation = Quaternion.Euler(0, 0, -75);
        }
        else 
        {
            if (attacking) { return; }
            graphicks.transform.localPosition = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if(hit_Able_F > 0 && !can_Hit)
        {
            attacking = true;
            graphicks_Script.is_Attacking = attacking;
            graphicks.transform.RotateAround(point_To_Rotate_Around.position, Vector3.forward, rotation_F);
            Debug.Log("attacking");
        }
    }

    public bool Shoot(GameObject target)
    {
        if (!can_Hit) { return false; }

        //rotate sword graphicks
        //hit
        //enable collider of sword
        source.PlayOneShot(source.clip);

        can_Hit = false;
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
            attacking = false;
            graphicks_Script.is_Attacking = attacking;
        }
    }
}
