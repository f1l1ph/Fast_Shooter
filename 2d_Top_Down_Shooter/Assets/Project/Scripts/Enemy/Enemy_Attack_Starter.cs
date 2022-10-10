using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Starter : MonoBehaviour
{
    private Enemy_AI_Basic parent_Enemy;

    private void Start()
    {
        parent_Enemy = GetComponentInParent<Enemy_AI_Basic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Attacking starter");
            parent_Enemy.SetTargetAndAttack(collision.transform);
        }
    }
}
