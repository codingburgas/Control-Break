using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Transform Player;

    private CharacterController2D CharacterController;
    private CameraController2D CameraController;
    private DeathManager DeathManager;

    private Vector3 LatestCheckpoint;

    void Start()
    {
        Player = GameObject.Find("Player").transform;

        CharacterController = Player.GetComponent<CharacterController2D>();
        CameraController = GameObject.Find("CameraController2D").GetComponent<CameraController2D>();
        DeathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();

        LatestCheckpoint = Player.position;
    }

    void Update()
    {
        LatestCheckpoint = CharacterController.Checkpoint;
    }

    public void Restart()
    {
        DeathManager.Revive();

        if (CharacterController.IsFlipped)
            CharacterController.Flip();
        
        Player.position = LatestCheckpoint;
    }
}
