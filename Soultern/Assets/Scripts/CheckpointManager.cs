using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Transform Player;

    [HideInInspector] public Vector3 StartingCheckpoint;
    [HideInInspector] public Vector3 LatestCheckpoint;

    [HideInInspector] public int MaxLives;
    [HideInInspector] public int Lives;

    void Awake()
    {
        if (Object.FindObjectOfType<CheckpointManager>() != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        Player = GameObject.Find("Player").transform;
        
        StartingCheckpoint = Player.transform.position;
    }

    void Start()
    {
        switch (PlayerPrefs.GetInt("Difficulty"))
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
        }

        Lives = MaxLives;

        LatestCheckpoint = Player.position;
    }
}