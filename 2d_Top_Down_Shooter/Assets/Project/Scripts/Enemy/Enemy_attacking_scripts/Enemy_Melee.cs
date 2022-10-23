using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;

public class Enemy_Melee : MonoBehaviour, IAttack_Able
{
    [SerializeField] private float reload_Time      = 1.5f;
    [SerializeField] private float damage           = 10;
    [SerializeField] private float attack_Distance  = 2f;

    [SerializeField] private GameObject impact_Effect;
    [SerializeField] private Transform  child_Orientation;
    [SerializeField] private Transform  shoot_Pos;
    [SerializeField] private LayerMask  player_Mask;

    [SerializeField] private BoxCollider2D boxCollider2d;

    private float time_To_Reload;
    private bool have_Attacked = false;

    private void Start()
    {
        time_To_Reload = 0.01f;
    }

    private void Update()
    {
        //just a timer for reloading
        if (have_Attacked && time_To_Reload > 0)
        {
            time_To_Reload -= Time.deltaTime;
        }
        if(time_To_Reload <= 0 && have_Attacked)
        {
            have_Attacked = false;
            time_To_Reload = reload_Time;
        }
    }

    public void Attack(GameObject target)
    {
        if (have_Attacked) { return; }

        have_Attacked = true;
        //apply animation

        //to make raycast we need to disable and enable collider
        //we dont want to shoot On Enemy
        boxCollider2d.enabled = false;
        //make raycast
        RaycastHit2D hit = Physics2D.Raycast(shoot_Pos.transform.position, 
                                             child_Orientation.right.normalized, 
                                             attack_Distance
                                             );

        Debug.DrawRay(shoot_Pos.transform.position, child_Orientation.right.normalized, Color.green);

        //check if hitted something
        if(hit.transform == null) { return; }

        //check if something is player
        float distance_Between_Objects = Vector2.Distance(hit.transform.position, transform.position);

        if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Player") && distance_Between_Objects <= attack_Distance)
        {
            //give damage
            IDamageAble player = hit.transform.GetComponent<IDamageAble>();
            player.Get_Damage(damage);

            //impact effect
            GameObject instance = Instantiate(impact_Effect, hit.point, Quaternion.identity);
            Destroy(instance, 0.3f);
        }
        boxCollider2d.enabled = true;
    }

}
