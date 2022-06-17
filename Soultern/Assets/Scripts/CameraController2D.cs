using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    private Transform MainCamera;

    private Transform Player;

    public Vector3 CameraOffset;
    public float SmoothEffect = 0.25f;

    void Start()
    {
        MainCamera = Camera.main.transform;
    }

    void FixedUpdate()
    {
        Player = GameObject.Find("Player").transform;
        
        if (Player.position.y >= -1.77f)
            SetCameraPosition();
    }

    void SetCameraPosition()
    {
        Vector3 velocity = Vector3.zero;

        MainCamera.position = Vector3.SmoothDamp(MainCamera.position, Player.position + CameraOffset, ref velocity, SmoothEffect);
    }
}
