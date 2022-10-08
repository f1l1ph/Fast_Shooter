using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float max_Velocity = 25f;
    [SerializeField] private float bullet_Detroy_Time = 3f;
    [SerializeField] private float damage = 20;
    [SerializeField] private float force_To_Hit = 1;

    [SerializeField] private GameObject impact_Effetct;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, bullet_Detroy_Time);
    }

    private void FixedUpdate()
    {
        Vector2 max_Rb_Velocity = Vector2.ClampMagnitude(rb.velocity, max_Velocity);
        rb.velocity = max_Rb_Velocity;
    }

    //can also set damage if needed 
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) { return; }//this should ignore trigger colliders

        //gives damage to IDamageAble
        if(collision.GetComponent<IDamageAble>() != null)
        {
            collision.GetComponent<IDamageAble>().Get_Damage(damage);
        }
        //gives force to rigidbody
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            collision.GetComponent<Rigidbody2D>().AddForce(transform.position * force_To_Hit, ForceMode2D.Impulse);
        }

        //apply vfx
        GameObject efct = Instantiate(impact_Effetct, gameObject.transform.position, Quaternion.identity);

        Destroy(efct, 0.3f);
        Destroy(gameObject);    

    }
}
