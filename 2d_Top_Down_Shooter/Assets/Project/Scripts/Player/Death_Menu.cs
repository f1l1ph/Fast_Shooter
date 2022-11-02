using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_Menu : MonoBehaviour
{
    [SerializeField] private GameObject death_Menu;
    public static Death_Menu Death_Menu_Instance { get; private set; }
    private void Awake()
    {
        if (Death_Menu_Instance != null && Death_Menu_Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Death_Menu_Instance = this;
        }

        death_Menu.SetActive(false);
    }


    public void Activate()
    {
        death_Menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Reset_Level()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Main_Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
