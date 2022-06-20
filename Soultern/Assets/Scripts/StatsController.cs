using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    private CharacterController2D CharacterController;
    private ExtraFunctions ExtraFunctions;

    private string Player = "Player";

    [HideInInspector] public int Health;
    [HideInInspector] public int HealthMax = 3;
    // [HideInInspector]
    public int Stamina = 0;
    [HideInInspector] public int StaminaMax = 360;

    private int JumpDrain = 90;

    private bool CanRegen;

    void Start()
    {
        CharacterController = GameObject.Find(Player).GetComponent<CharacterController2D>();
        ExtraFunctions = GameObject.Find("ExtraFunctions").GetComponent<ExtraFunctions>();

        Health = HealthMax;
        Stamina = StaminaMax;
    }
    
    void FixedUpdate()
    {
        if (Stamina < StaminaMax)
            CanRegen = true;
        else
            CanRegen = false;

        CharacterController.CanUse = Stamina > 0;

        if (Input.GetKey(CharacterController.SprintKey))
        {
            if (CharacterController.CanUse)
            {
                CanRegen = false;
                DrainStamina();
            }
        }

        if (CanRegen && Mathf.Abs(CharacterController.GetMovement()) < 0.1f && CharacterController.IsGrounded)
            RegenStamina();
    }

    void Update()
    {
        if (Input.GetKeyDown(CharacterController.JumpKey))
        {
            if (CharacterController.IsGrounded && CharacterController.CanUse)
            {
                CanRegen = false;
                DrainJumpStamina();
            }
        }
    }

    void DrainStamina()
    {
        ExtraFunctions.Wait(StaminaMax / 360 / 6);
        Stamina--;
    }

    void DrainJumpStamina()
    {
        Stamina -= JumpDrain;
    }

    void RegenStamina()
    {
        ExtraFunctions.Wait(StaminaMax / 360 / 10);
        Stamina++;
    }
}
