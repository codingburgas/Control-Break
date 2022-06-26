using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private DeathManager DeathManager;
    private CheckpointManager CheckpointManager;
    private ExtraFunctions ExtraFunctions;

    private GameObject YouDiedMenu;
    private GameObject GameOverMenu;
    private GameObject PauseMenu;
    private GameObject StaminaBar;

    [HideInInspector] public bool YouDied;
    [HideInInspector] public bool IsGameOver;
    [HideInInspector] public bool IsPaused;

    [HideInInspector] public float AnimationTime;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Game") return;

        DeathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();
        CheckpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        ExtraFunctions = GameObject.Find("ExtraFunctions").GetComponent<ExtraFunctions>();

        YouDiedMenu = ExtraFunctions.FindInactive("You Died Menu");
        GameOverMenu = ExtraFunctions.FindInactive("Game Over Menu");
        PauseMenu = ExtraFunctions.FindInactive("Pause Menu");
        StaminaBar = GameObject.Find("Stamina Bar");

        YouDied = false;
        IsGameOver = false;
        IsPaused = false;

        AnimationTime = 2.0f;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Game") return;

        if (DeathManager.IsDead && !YouDied && CheckpointManager.Lives > 0)
            DisplayYouDiedMenu();

        if (DeathManager.IsDead && !IsGameOver && CheckpointManager.Lives == 0)
            DisplayGameOverMenu();

        if (Input.GetKeyDown(KeyCode.Escape))
            DisplayPauseMenu();
    }

    public void DisplayYouDiedMenu()
    {
        YouDiedMenu.SetActive(!YouDied);
        StaminaBar.SetActive(YouDied);
        Time.timeScale = System.Convert.ToSingle(YouDied);
        YouDied = !YouDied;
        PauseMusic();
    }

    public void DisplayGameOverMenu()
    {
        GameOverMenu.SetActive(!IsGameOver);
        StaminaBar.SetActive(IsGameOver);
        Time.timeScale = System.Convert.ToSingle(IsGameOver);
        IsGameOver = !IsGameOver;
        PauseMusic();
    }

    public void DisplayPauseMenu()
    {
        PauseMenu.SetActive(!IsPaused);
        StaminaBar.SetActive(IsPaused);
        Time.timeScale = System.Convert.ToSingle(IsPaused);
        IsPaused = !IsPaused;
        PauseMusic();
    }

    void PauseMusic()
    {
        AudioSource Music = Camera.main.GetComponent<AudioSource>();
        if (Music.isPlaying)
            Music.Pause();
        else
            Music.Play();
    }

    // Main Menu

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void Continue()
    {
        
    }

    public void Difficulty()
    {
        SceneManager.LoadScene("Difficulty", LoadSceneMode.Single);
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Difficulty", GameObject.Find("Difficulty").GetComponent<Dropdown>().value);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}