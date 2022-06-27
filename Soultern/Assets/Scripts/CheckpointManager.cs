using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Transform Player;

    [HideInInspector] public Vector3 StartingCheckpoint;
    [HideInInspector] public Vector3 LatestCheckpoint;

    [HideInInspector] public int Difficulty = 5;

    [HideInInspector] public int MaxLives;
    [HideInInspector] public int Lives;

    void Awake()
    {
        GameObject[] Objects = GameObject.FindGameObjectsWithTag("CheckpointManager");
 
        bool NotFirst = false;

        foreach (GameObject oneOther in Objects)
            if (oneOther.scene.buildIndex == -1)
                NotFirst = true;

        if (NotFirst == true)
            Destroy(gameObject);
        
        DontDestroyOnLoad(transform.gameObject);

        Difficulty = PlayerPrefs.GetInt("Difficulty");

        Player = GameObject.Find("Player").transform;

        StartingCheckpoint = Player.position;
    }

    void Start()
    {
        Difficulty = PlayerPrefs.GetInt("Difficulty");

        switch (Difficulty)
        {
            case 0:
                MaxLives = 5;
                break;
            case 1:
                MaxLives = 3;
                break;
            case 2:
                MaxLives = 1;
                break;
            default:
                MaxLives = 30;
                break;
        }

        Lives = MaxLives;

        LatestCheckpoint = Player.position;
    }
}