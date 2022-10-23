using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Energy : MonoBehaviour
{
    [SerializeField] private float max_Energy = 100;
    [SerializeField] private float current_Energy;
    [SerializeField] Slider energy_Slider;

    
    private void Start()
    {
        energy_Slider.maxValue = max_Energy;
        Update_Energy();
    }

    public void Consume_Energy(float energy)
    {
        current_Energy -= Mathf.Abs(energy);
        Update_Energy();
    }

    public void Add_Energy(float energy)
    {
        current_Energy += Mathf.Abs(energy);

        Update_Energy();
    }

    private void Update_Energy()
    {
        Mathf.Clamp(current_Energy, current_Energy, max_Energy);
        energy_Slider.value = current_Energy;
    }

    public float GetEnergy()
    {
        return current_Energy;
    }
}
