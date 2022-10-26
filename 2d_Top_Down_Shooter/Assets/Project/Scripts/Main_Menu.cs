using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    
    public void Play(int scene_Number)
    {   
        SceneManager.LoadScene(scene_Number);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
