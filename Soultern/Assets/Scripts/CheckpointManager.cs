using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Transform Player;

    private CharacterController2D CharacterController;
    private CameraController2D CameraController;
    private DeathManager DeathManager;

    [HideInInspector] public Vector3 StartingCheckpoint;
    [HideInInspector] public Vector3 LatestCheckpoint;

    [HideInInspector] public int Lives = 3;

    void Awake()
    {
        if (Object.FindObjectOfType<CheckpointManager>() != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        Player = GameObject.Find("Player").transform;
        
        StartingCheckpoint = Player.transform.position; //new Vector3(-193.27f, 0f, 0f)
    }

    void Start()
    {
        LatestCheckpoint = Player.position;

        CharacterController = Player.GetComponent<CharacterController2D>();
        CameraController = GameObject.Find("CameraController2D").GetComponent<CameraController2D>();
        DeathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();
    }
}