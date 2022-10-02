using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour, IDamageAble
{
    [SerializeField] private float Max_Health = 100;
    [SerializeField] private float current_Health;

    private void Start()
    {
        if (current_Health > Max_Health) { current_Health = Max_Health; }
        if (current_Health < 0) { Kill(); }
    }
    

    public void Get_Damage(float damage)
    {
        current_Health -= Mathf.Abs(damage);
        if (current_Health <= 0) { Kill(); }
    }

    public void Heal(float health)
    {
        current_Health += Mathf.Abs(health);
        Mathf.Clamp(current_Health, current_Health, Max_Health);
    }

    private void Kill()
    {
        Debug.Log("enemy died");


        Destroy(gameObject);
    }

}
