using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asset_ : MonoBehaviour
{
    public Collectibles collectible = Collectibles.health;
    [SerializeField] private float amount;

    public float Action()
    {
        Destroy(gameObject, 0.1f);
        return amount;
    }
}