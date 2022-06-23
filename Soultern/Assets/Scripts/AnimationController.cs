using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private GameObject Player;
    private GameObject Woodsman;

    private Animator PlayerAnimator;
    private Animator WoodsmanAnimator;

    private CharacterController2D CharacterController;
    private WoodsmanController WoodsmanController;
    private StatsController StatsController;

    void Start()
    {
        Player = GameObject.Find("Player");
        Woodsman = GameObject.Find("Woodsman");

        PlayerAnimator = Player.GetComponent<Animator>();
        WoodsmanAnimator = Woodsman.GetComponent<Animator>();
        
        CharacterController = Player.GetComponent<CharacterController2D>();
        WoodsmanController = Woodsman.GetComponent<WoodsmanController>();
        StatsController = GameObject.Find("StatsController").GetComponent<StatsController>();
    }

    void Update()
    {
        PlayerAnimator.SetBool("IsWalking", CharacterController.IsWalking);
        PlayerAnimator.SetFloat("VelocityY", CharacterController.GetVelocity().y);
        PlayerAnimator.SetBool("TakenDamage", CharacterController.TakeDamage);
        PlayerAnimator.SetInteger("Health", StatsController.Health);
        
        WoodsmanAnimator.SetBool("WillThrow", WoodsmanController.WillThrowAxe);
    }
}