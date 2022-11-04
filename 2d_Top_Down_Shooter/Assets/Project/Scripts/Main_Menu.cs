using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    [SerializeField] private GameObject main_Menu;
    [SerializeField] private GameObject options_Menu;

    private void Start()
    {
        options_Menu.SetActive(false);
    }

    public void Play(int scene_Number)
    {   
        SceneManager.LoadScene(scene_Number);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        options_Menu.SetActive(true);
        main_Menu.SetActive(false);
    }
    
    public void Back_To_Main_Menu()
    {
        main_Menu.SetActive(true);
        options_Menu.SetActive(false);
    }

}
