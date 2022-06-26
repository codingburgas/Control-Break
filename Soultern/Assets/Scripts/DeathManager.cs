using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    private StatsController StatsController;
    private CheckpointManager CheckpointManager;
    private MenuManager MenuManager;
    private ExtraFunctions ExtraFunctions;

    private Transform Player;

    private GameObject LivesText;

    [HideInInspector] public bool IsDead;

    private float AnimationTime = 0.0f;
    private float DeathLevel = -9.15f;

    void Start()
    {
        StatsController = GameObject.Find("StatsController").GetComponent<StatsController>();
        CheckpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        MenuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        ExtraFunctions = GameObject.Find("ExtraFunctions").GetComponent<ExtraFunctions>();

        Player = GameObject.Find("Player").transform;

        LivesText = ExtraFunctions.FindInactive("Lives");
    }

    void Update()
    {
        if (Player.position.y <= DeathLevel)
        {
            AnimationTime = 0f;
            StatsController.Health = 0;
        }

        if (StatsController.Health == 0 && !IsDead)
            StartCoroutine(ToggleDeath());

        if (CheckpointManager.Lives == 0)
            CheckpointManager.LatestCheckpoint = CheckpointManager.StartingCheckpoint;

        LivesText.GetComponent<Text>().text = "x" + CheckpointManager.Lives;
    }

    public void Revive()
    {
        if (CheckpointManager.Lives == 0)
            CheckpointManager.Lives = CheckpointManager.MaxLives;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ToggleDeath()
    {
        yield return new WaitForSeconds(AnimationTime);
        if (!IsDead)
            CheckpointManager.Lives--;
        IsDead = true;
    }
}