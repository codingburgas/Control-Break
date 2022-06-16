using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    private CharacterController2D CharacterController;

    private string Player = "Player";

    private int Health;
    private int HealthMax = 3;
    public int Stamina = 0;
    [HideInInspector] public int StaminaMax = 360;

    private bool CanRegen;

    void Start()
    {
        CharacterController = GameObject.Find(Player).GetComponent<CharacterController2D>();

        Health = HealthMax;
        Stamina = StaminaMax;
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
        Wait(StaminaMax / 360 / 6);
        Stamina--;
    }

    void RegenStamina()
    {
        Wait(StaminaMax / 360 / 10);
        Stamina++;
    }

    IEnumerator Wait(int time)
    {
        yield return new WaitForSeconds(time);
    }
}
