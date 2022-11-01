using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour, IDamageAble
{
    [SerializeField] private float  Max_Health = 100;
    [SerializeField] private float  current_Health;
    [SerializeField] Slider         health_Slider;

    private void Start()
    {
        if(current_Health > Max_Health) { current_Health = Max_Health; }
        if(current_Health < 0) { Kill(); }

        health_Slider.maxValue = Max_Health;

        UpdateAndCheckForHealth();
    }

    public void Get_Damage(float damage)
    {
        current_Health -= Mathf.Abs(damage);
        UpdateAndCheckForHealth();
    }

    public void Heal(float health)
    {
        current_Health += Mathf.Abs(health);

        UpdateAndCheckForHealth();
    }

    private void Kill()
    {
        Debug.LogError("You died");
    }

    private void UpdateAndCheckForHealth()
    {
        current_Health = Mathf.Clamp(current_Health, 0, Max_Health);
        health_Slider.value = current_Health;

        if (current_Health <= 0) { Kill(); }
    }
}
