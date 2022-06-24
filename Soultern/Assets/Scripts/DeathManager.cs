using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    private StatsController StatsController;
    private CharacterController2D CharacterController;
    private MenuManager MenuManager;
    private ExtraFunctions ExtraFunctions;

    private Transform Player;

    [HideInInspector] public bool IsDead;

    private float DeathLevel = -9.15f;

    void Start()
    {
        StatsController = GameObject.Find("StatsController").GetComponent<StatsController>();
        CharacterController = GameObject.Find("Player").GetComponent<CharacterController2D>();
        MenuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        ExtraFunctions = GameObject.Find("ExtraFunctions").GetComponent<ExtraFunctions>();

        Player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (Player.position.y <= DeathLevel)
            StatsController.Health = 0;

        if (StatsController.Health == 0)
            IsDead = true;
    }

    public void Revive()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}