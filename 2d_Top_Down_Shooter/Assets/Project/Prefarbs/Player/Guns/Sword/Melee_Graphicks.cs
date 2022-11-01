using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Graphicks : MonoBehaviour
{
    public bool is_Attacking = false;
    [HideInInspector] public float damage = 0;
    [HideInInspector] public float force_To_Hit = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!is_Attacking) { return; }
        if(collision.tag != "Player") { return; }

        if (collision.isTrigger) { return; }

        if (collision.GetComponent<IDamageAble>() != null)
        {
            collision.GetComponent<IDamageAble>().Get_Damage(damage);
        }

        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            collision.GetComponent<Rigidbody2D>().AddForce(transform.position * force_To_Hit, ForceMode2D.Impulse);
        }
    }
}
