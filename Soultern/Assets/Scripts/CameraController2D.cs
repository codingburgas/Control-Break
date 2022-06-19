using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    private Transform MainCamera;

    private Transform Player;

    private Vector3 CameraOffset = new Vector3(0, 3.3f, -10);
    [HideInInspector] public float SmoothEffect = 0.2125f;

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
