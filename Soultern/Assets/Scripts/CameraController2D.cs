using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    private Transform MainCamera;
    private AudioSource Music;

    private Transform Player;

    private Vector3 CameraOffset = new Vector3(0, 3.3f, -10);
    
    [HideInInspector] private float SmoothEffect = 0.165f;

    void Start()
    {
        MainCamera = Camera.main.transform;

        Music = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        Player = GameObject.Find("Player").transform;
        
        SetCameraPosition();
    }

    void SetCameraPosition()
    {
        Vector3 velocity = Vector3.zero;

        Vector3 NewPostion = Vector3.SmoothDamp(MainCamera.position, Player.position + CameraOffset, ref velocity, SmoothEffect);
        MainCamera.position = new Vector3(NewPostion.x, MainCamera.position.y, MainCamera.position.z);
    }
}
