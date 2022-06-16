using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator ObjectAnimator;

    private string MainPlayer = "Player";

    private CharacterController2D CharacterController;

    void Start()
    {
        ObjectAnimator = GameObject.Find(MainPlayer).GetComponent<Animator>();;
        
        CharacterController = GameObject.Find(MainPlayer).GetComponent<CharacterController2D>();
    }

    void Update()
    {
        ObjectAnimator.SetBool("IsWalking", CharacterController.IsWalking);
    }
}