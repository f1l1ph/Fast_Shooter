using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this is class for interfaces, don't delete it is important!!!
/// </summary>
public class Interfaces : MonoBehaviour
{
    
}


interface IGun
{
    int energy_Needed_To_Shoot { get; set; }

    int inventory_Position { get; set; }
    RectTransform ui_Element { get; set; }

    GameObject this_Gameobject { get; set; }

    public bool Shoot(GameObject target);
}
interface IDamageAble
{
    public void Get_Damage(float damage);

    public void Heal(float health);
}

interface IAttack_Able
{
    public void Attack(GameObject target);
}
