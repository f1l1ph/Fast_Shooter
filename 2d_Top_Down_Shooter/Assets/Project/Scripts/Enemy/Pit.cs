using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    [SerializeField] private GameObject strong_Enemy;
    [SerializeField] private GameObject normal_Enemy;

    [SerializeField] private Door[] doors;

    [SerializeField] private Transform[] spawn_Positions;

    [SerializeField] private int max_Waves;
    [SerializeField] private int wave_Count = 0;

    bool player_Entered = false;

    GameObject[] enemies;

    GameObject[] strong_Enemies;

    private void Start()
    {
        enemies = new GameObject[spawn_Positions.Length];
        strong_Enemies = new GameObject[spawn_Positions.Length];
    }


    public void Player_Entered()
    {
        Start_Wave();
        player_Entered = true;
        Chcek_For_Wave();
    }

    private void Start_Wave()
    {
        //if(wave_Count >= max_Waves)

        Debug.Log("Started wave");

        for (int i = 0; i <= spawn_Positions.Length-1; i++)
        {
            GameObject enemy = Instantiate(normal_Enemy, spawn_Positions[i].position, Quaternion.identity);
            enemies[i] = enemy;

            if (Random.Range(0, 10 - wave_Count) == 10 - wave_Count - 1) 
            {
                GameObject strong_Enemy_Instance = Instantiate(strong_Enemy, spawn_Positions[i].position, Quaternion.identity);
                strong_Enemies[i] = strong_Enemy_Instance;
                Debug.Log("strong enemy");
            }
        }

        wave_Count++;
    }

    private void Chcek_For_Wave()
    {

        if (player_Entered == false) { return; }

        Invoke("Chcek_For_Wave", 0.1f);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null) { return; }

            if (wave_Count >= max_Waves)
            {
                for (int x = 0; x < doors.Length; x++)
                {
                    doors[x].Open();
                }
                return;
            }

            if (enemies[i] == null && i >= enemies.Length - 1) { Start_Wave(); } 
        }
    }
}
