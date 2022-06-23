using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private DeathManager DeathManager;
    private ExtraFunctions ExtraFunctions;

    private GameObject GameOverMenu;
    private GameObject PauseMenu;
    private GameObject StaminaBar;

    [HideInInspector] public bool IsGameOver;
    [HideInInspector] public bool IsPaused;

    void Start()
    {
        DeathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();
        ExtraFunctions = GameObject.Find("ExtraFunctions").GetComponent<ExtraFunctions>();

        GameOverMenu = ExtraFunctions.FindInactive("Game Over Menu");
        PauseMenu = ExtraFunctions.FindInactive("Pause Menu");
        StaminaBar = GameObject.Find("Stamina Bar");
    }

    void Update()
    {
        IsGameOver = GameOverMenu.activeSelf;
        IsPaused = PauseMenu.activeSelf;

        if (DeathManager.IsDead && !IsGameOver)
            DisplayGameOverMenu();

        if (Input.GetKeyDown(KeyCode.Escape))
            DisplayPauseMenu();
    }

    public void DisplayGameOverMenu()
    {
        GameOverMenu.SetActive(!IsGameOver);
        StaminaBar.SetActive(IsGameOver);
        Time.timeScale = System.Convert.ToSingle(IsGameOver);
    }

    public void DisplayPauseMenu()
    {
        PauseMenu.SetActive(!IsPaused);
        StaminaBar.SetActive(IsPaused);
        Time.timeScale = System.Convert.ToSingle(IsPaused);
    }
}
