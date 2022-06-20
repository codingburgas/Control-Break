using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator ObjectAnimator;

    private string Player = "Player";

    private CharacterController2D CharacterController;

    void Start()
    {
        ObjectAnimator = GameObject.Find(Player).GetComponent<Animator>();
        
        CharacterController = GameObject.Find(Player).GetComponent<CharacterController2D>();
    }

    void Update()
    {
        ObjectAnimator.SetBool("IsWalking", CharacterController.IsWalking);
        ObjectAnimator.SetFloat("VelocityY", CharacterController.GetVelocity().y);
    }
}