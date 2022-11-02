using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Level_Completed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_Object;
    [SerializeField] private string text;
    [SerializeField] private float time_To_Wait;

    [Tooltip("Enter scene index of scene you want to load")] [SerializeField] private int scene_To_Load;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            text_Object.gameObject.SetActive(true);
            text_Object.text = text;
            StartCoroutine(Load_Next_Level());
        }
    }

    IEnumerator Load_Next_Level()
    {
        yield return new WaitForSeconds(time_To_Wait);
        SceneManager.LoadScene(scene_To_Load);
    }
}
