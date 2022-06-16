using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private StatsController StatsController;

    private Slider Slider;

    void Start()
    {
        StatsController = GameObject.Find("StatsController").GetComponent<StatsController>();

        Slider = GameObject.Find("Stamina Bar").GetComponent<Slider>();

        Slider.maxValue = StatsController.StaminaMax;
    }

    void Update()
    {
        SetStamina(StatsController.Stamina);
    }

    void SetStamina(int stamina)
    {
        Slider.value = stamina;
    }
}
