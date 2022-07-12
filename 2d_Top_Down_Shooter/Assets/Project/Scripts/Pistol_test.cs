using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pistol_test : MonoBehaviour, IGun
{
    [Header("Shooting")]
    [SerializeField] private float shoot_Distance;
    [SerializeField] private Transform shoot_Pos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bullet_Speed;
    [SerializeField] private float shots_Per_Second;


    private bool can_Shoot = true;
    private float shoot_Able_F;

    private void Start()
    {
        shoot_Able_F = shots_Per_Second;        
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
        RaycastHit2D hit = Physics2D.Raycast(shoot_Pos.transform.position, aim.transform.localPosition * shoot_Distance, shoot_Distance);
        Debug.DrawRay(shoot_Pos.transform.position, aim.transform.localPosition * shoot_Distance, Color.green, shoot_Distance);

        if (hit.transform != null)
        {
            //hitting logic in here
        }

        GameObject bullet_Instance = Instantiate(bullet, shoot_Pos.position, transform.parent.transform.rotation);
        Rigidbody2D rb_Bullet = bullet_Instance.GetComponent<Rigidbody2D>();

        rb_Bullet.AddForce(aim.transform.localPosition * bullet_Speed, ForceMode2D.Impulse);
        can_Shoot = false;
    }
}
