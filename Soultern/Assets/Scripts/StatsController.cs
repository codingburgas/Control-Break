using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    private CharacterController2D CharacterController;

    private string Player = "Player";

    private int Health;
    private int HealthMax = 3;
    public int Stamina;
    private int StaminaMax;

    private int StaminaLength = 6;
    private int StaminaRegenTime = 10;
    private int StaminaRegen;

    private bool CanRegen;

    void Start()
    {
        CharacterController = GameObject.Find(Player).GetComponent<CharacterController2D>();

        Health = HealthMax;
        Stamina = StaminaMax;

        StaminaMax = StaminaLength * 60;
        StaminaRegen = StaminaMax / StaminaRegenTime;
    }
    
    void FixedUpdate()
    {
        if (Stamina < StaminaMax)
            CanRegen = true;
        else
            CanRegen = false;

        CharacterController.CanSprint = Stamina > 0;

        if (Input.GetKey(CharacterController.SprintKey))
        {
            if (CharacterController.CanSprint)
                DrainStamina();
            CanRegen = false;
        }

        if (CanRegen)
            RegenStamina();
    }

    void DrainStamina()
    {
        Wait(StaminaMax / (StaminaLength - 1) / 360);
        Stamina--;
    }

    void RegenStamina()
    {
        Wait(StaminaMax / (StaminaRegenTime - 1) / 360);
        Stamina++;
    }

    IEnumerator Wait(int time)
    {
        yield return new WaitForSeconds(time);
    }
}
