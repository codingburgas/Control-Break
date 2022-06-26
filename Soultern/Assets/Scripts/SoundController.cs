using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    private MusicController MusicController;

    private Slider MasterVolume;
    private Slider MusicVolume;

    private float MasterVolumeValue = 100;
    private float MusicVolumeValue = 100;

    void Start()
    {
        MasterVolumeValue = PlayerPrefs.GetFloat("Master Volume");
        MusicVolumeValue = PlayerPrefs.GetFloat("Music Volume");

        if (SceneManager.GetActiveScene().name == "Game") return;

        MusicController = GameObject.Find("Music").GetComponent<MusicController>();

        if (SceneManager.GetActiveScene().name != "Settings") return;

        MasterVolume = GameObject.Find("Master Volume").transform.Find("Slider").GetComponent<Slider>();
        MusicVolume = GameObject.Find("Music Volume").transform.Find("Slider").GetComponent<Slider>();

        MasterVolume.value = MasterVolumeValue;
        MusicVolume.value = MusicVolumeValue;
    }

    void Update()
    {
        AudioListener.volume = MasterVolumeValue / 100;
        
        if (SceneManager.GetActiveScene().name != "Game")
            MusicController.Music.volume = MusicVolumeValue / 100;
        else
            Camera.main.GetComponent<AudioSource>().volume = MusicVolumeValue / 100;

        if (SceneManager.GetActiveScene().name != "Settings") return;

        MasterVolumeValue = MasterVolume.value;
        MusicVolumeValue = MusicVolume.value;

        PlayerPrefs.SetFloat("Master Volume", MasterVolumeValue);
        PlayerPrefs.SetFloat("Music Volume", MusicVolumeValue);
    }
}