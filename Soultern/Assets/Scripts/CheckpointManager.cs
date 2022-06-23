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
        if (FindObjectsOfType(GetType()).Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Player = GameObject.Find("Player").transform;

        LatestCheckpoint = Player.position;

        CharacterController = Player.GetComponent<CharacterController2D>();
        CameraController = GameObject.Find("CameraController2D").GetComponent<CameraController2D>();
        DeathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();
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

        CharacterController.TakeDamage = false;
    }

    public void TeleportToCheckpoint()
    {
        Player.position = LatestCheckpoint;
    }
}