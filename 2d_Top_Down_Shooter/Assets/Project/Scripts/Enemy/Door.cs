using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isDoor;
    [SerializeField] private Pit  pit;
    [SerializeField] private EdgeCollider2D edgeCollider;
    [SerializeField] private GameObject graphicks;

    private void Start()
    {
        if (isDoor) { graphicks.SetActive(false); edgeCollider.isTrigger = true; }
        else { Close(); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isDoor) { return; }

        if (collision.tag == "Player")
        {
            pit.Player_Entered();
            Close();
        }
    }

    public void Close()
    {
        edgeCollider.isTrigger = false;
        graphicks.SetActive(true);
    }

    public void Open()
    {
        if (isDoor) { return; }
        edgeCollider.isTrigger = true;
        graphicks.SetActive(false);
    }
}
