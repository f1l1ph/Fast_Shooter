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
    void Shoot(GameObject target)
    {
        Debug.Log("Not implemented interface 'Shoot'");
    }

}



