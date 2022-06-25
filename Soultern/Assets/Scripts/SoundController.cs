using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private CameraController2D CameraController;

    void Start()
    {
        CameraController = GameObject.Find("CameraController2D").GetComponent<CameraController2D>();
    }

    void Update()
    {
        // AudioListener.volume = 0;
    }
}