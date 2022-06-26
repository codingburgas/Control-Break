using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    [HideInInspector] public AudioSource Music;

    void Awake()
    {
        GameObject[] Objects = GameObject.FindGameObjectsWithTag("Music");
 
        bool NotFirst = false;

        foreach (GameObject oneOther in Objects)
            if (oneOther.scene.buildIndex == -1)
                NotFirst = true;

        if (NotFirst == true)
            Destroy(gameObject);
        
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            Music = Camera.main.GetComponent<AudioSource>();
            GetComponent<AudioSource>().volume = 0f;
            return;
        }

        if (SceneManager.GetActiveScene().name != "Game")
            Music = GetComponent<AudioSource>();
    }
}
