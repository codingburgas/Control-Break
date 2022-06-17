using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private StatsController StatsController;

    private Transform Player;

    [HideInInspector] public bool IsDead;

    private float DeathLevel = -9.15f;

    void Start()
    {
        StatsController = GameObject.Find("StatsController").GetComponent<StatsController>();

        Player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (Player.position.y <= DeathLevel)
            StatsController.Health = 0;

        if (StatsController.Health == 0)
            IsDead = true;
    }

    void Respawn()
    {
        StatsController.Health = StatsController.HealthMax;
        StatsController.Stamina = StatsController.StaminaMax;
        
        IsDead = false;
    }
}