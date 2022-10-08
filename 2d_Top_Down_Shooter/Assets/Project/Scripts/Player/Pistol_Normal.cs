using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Pick_Up))]
public class Pistol_Normal : MonoBehaviour, IGun
{
    [Header("Shooting")]
    [SerializeField] private float      shoot_Distance;
    [SerializeField] private Transform  shoot_Pos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float      bullet_Speed = 25;
    [SerializeField] private float      shots_Per_Second = 0.5f;
    [SerializeField] private float      recoil_Force = 1;

    [SerializeField] private RectTransform ui_element;

    private bool can_Shoot = true;
    private float shoot_Able_F;

    public int inventory_Position { get; set; }

    public RectTransform ui_Element { get; set; }

    public GameObject this_Gameobject { get; set; }

    private void Awake()
    {
        this_Gameobject = gameObject;
        shoot_Able_F = shots_Per_Second;
        ui_Element = ui_element;
    }

    private void Update()
    {
        Shoot_Time_Check();
    }

    private void Shoot_Time_Check()
    {
        if (!can_Shoot && shoot_Able_F > 0)
        {
            shoot_Able_F -= Time.deltaTime;
        }
        else if (shoot_Able_F <= 0)
        {
            can_Shoot = true;
            shoot_Able_F = shots_Per_Second;
        }
    }

    public void Shoot(GameObject aim)
    {
        if (!can_Shoot) { return; }
       
        GameObject bullet_Instance = Instantiate(bullet, shoot_Pos.position, transform.parent.transform.rotation);
        Rigidbody2D rb_Bullet = bullet_Instance.GetComponent<Rigidbody2D>();

        rb_Bullet.AddForce(transform.right * bullet_Speed, ForceMode2D.Impulse);

        //for adding recoil
        Transform container_Transform = gameObject.GetComponentInParent<Transform>();
        Rigidbody2D player_Rb = container_Transform.GetComponentInParent<Rigidbody2D>();

        player_Rb.AddForce(transform.right * recoil_Force * -1, ForceMode2D.Impulse);
        
        can_Shoot = false;
    }
}
