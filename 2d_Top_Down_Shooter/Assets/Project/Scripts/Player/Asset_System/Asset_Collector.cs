using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public enum Collectibles
{
    health,
    energy,
    coins
}

public class Asset_Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.GetComponent<Asset_>() != null)
        {
            Asset_ asset = other.transform.GetComponent<Asset_>();
            if(asset.collectible == Collectibles.health)
            {
                transform.GetComponent<Player_Health>().Heal(asset.Action());
            }
        }
    }
}
