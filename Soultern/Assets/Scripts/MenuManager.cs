using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private DeathManager DeathManager;

    private GameObject GameOverMenu;
    private GameObject PauseMenu;
    private GameObject StaminaBar;

    private bool IsGameOver;
    private bool IsPaused;

    void Start()
    {
        DeathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();

        GameOverMenu = FindInactive("Game Over Menu");
        PauseMenu = FindInactive("Pause Menu");
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

    void DisplayGameOverMenu()
    {
        GameOverMenu.SetActive(!IsGameOver);
        StaminaBar.SetActive(IsGameOver);
        Time.timeScale = System.Convert.ToSingle(IsGameOver);
    }

    void DisplayPauseMenu()
    {
        PauseMenu.SetActive(!IsPaused);
        StaminaBar.SetActive(IsPaused);
        Time.timeScale = System.Convert.ToSingle(IsPaused);
    }

    GameObject FindInactive(string name)
    {
        Transform[] Objects = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];

        for (int i = 0; i < Objects.Length; i++)
        {
            if (Objects[i].name == name)
                return Objects[i].gameObject;
        }

        return null;
    }
}
