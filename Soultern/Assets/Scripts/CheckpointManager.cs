using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Transform Player;

    private CharacterController2D CharacterController;
    private CameraController2D CameraController;
    private DeathManager DeathManager;

    public Vector3 LatestCheckpoint;

    void Awake()
    {
        if (Object.FindObjectOfType<CheckpointManager>() != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Player = GameObject.Find("Player").transform;

        LatestCheckpoint = Player.position;

        CharacterController = Player.GetComponent<CharacterController2D>();
        CameraController = GameObject.Find("CameraController2D").GetComponent<CameraController2D>();
        DeathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();
    }
}