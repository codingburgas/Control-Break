using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private RawImage Background;

    private Vector2 ScrollSpeed;

    void Start()
    {
        Background = GameObject.Find("Background").GetComponent<RawImage>();

        ScrollSpeed = new Vector2( 0.03f, 0f );
    }

    void Update()
    {
        ScrollBackground();
    }

    void ScrollBackground()
    {
        Background.uvRect = new Rect(Background.uvRect.position + ScrollSpeed * Time.deltaTime, Background.uvRect.size);
    }

    public void Continue()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}