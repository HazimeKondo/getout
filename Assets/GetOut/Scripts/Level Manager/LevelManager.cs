using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; set; }

    public GameObject EnemiesHolder;

    public Level LevelData;

    private static int CurrentLevel = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void WinLevel()
    {
        CurrentLevel++;
    }

    public void PrepareLevel()
    {
        Instantiate(LevelData.LevelsList[CurrentLevel], EnemiesHolder.transform);
    }
}
