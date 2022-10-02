using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float max_Velocity = 25f;
    [SerializeField] private float bullet_Detroy_Time = 3f;
    [SerializeField] private float damage = 20;

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

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) { return; }//this should ignore trigger colliders

        if(collision.GetComponent<IDamageAble>() != null)
        {
            collision.GetComponent<IDamageAble>().Get_Damage(damage);
        }

        //apply vfx
        GameObject efct = Instantiate(impact_Effetct, gameObject.transform.position, Quaternion.identity);

        Destroy(efct, 0.3f);
        Destroy(gameObject);    

    }
}
