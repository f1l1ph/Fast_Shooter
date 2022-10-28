using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour, IDamageAble
{
    [SerializeField] private float Max_Health = 100;
    [SerializeField] private float current_Health;
    [SerializeField] private GameObject[] assets_To_Drop;
    [SerializeField] private float asset_Dropper = 3f;

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

        for (int i = 0; i <= assets_To_Drop.Length-1; i++)
        {
            for (int x = 0; x <= Random.Range(1, asset_Dropper); x++)
            {
                Instantiate(assets_To_Drop[i], transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0), Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}
