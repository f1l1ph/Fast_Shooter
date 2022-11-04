using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Graphicks : MonoBehaviour
{
    public bool is_Attacking = false;
    [HideInInspector] public float damage = 0;
    [HideInInspector] public float force_To_Hit = 0;
    [SerializeField] private GameObject impact_Effect;
    [SerializeField] private Transform point;


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(damage);

        if (!is_Attacking) { return; }
        if(collision.tag == "Player") { return; }

        if (collision.isTrigger) { return; }

        if (collision.GetComponent<IDamageAble>() != null)
        {
            collision.GetComponent<IDamageAble>().Get_Damage(damage);
            GameObject effct = Instantiate(impact_Effect, point.position, Quaternion.identity);
            Destroy(effct, 0.5f);
        }

        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            collision.GetComponent<Rigidbody2D>().AddForce(transform.position * force_To_Hit, ForceMode2D.Impulse);
        }
    }
}
