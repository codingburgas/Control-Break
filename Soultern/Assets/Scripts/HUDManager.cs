using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private StatsController StatsController;
    private ExtraFunctions ExtraFunctions;

    private GameObject HealthBar;
    private Slider StaminaBar;

    void Start()
    {
        StatsController = GameObject.Find("StatsController").GetComponent<StatsController>();
        ExtraFunctions = GameObject.Find("ExtraFunctions").GetComponent<ExtraFunctions>();

        HealthBar = GameObject.Find("Health Bar");
        StaminaBar = GameObject.Find("Stamina Bar").GetComponent<Slider>();

        StaminaBar.maxValue = StatsController.StaminaMax;
    }

    void Update()
    {
        SetHealth(StatsController.Health);

        SetStamina(StatsController.Stamina);
    }

    void SetHealth(int health)
    {
        for (int i = 1; i <= StatsController.HealthMax; i++)
        {
            GameObject HeartObject = GameObject.Find("Heart " + i);
            GameObject FullHeart = HeartObject.transform.Find("Full Heart").gameObject;
            GameObject EmptyHeart = HeartObject.transform.Find("Empty Heart").gameObject;

            bool ShouldShow = i <= health;
            FullHeart.SetActive(ShouldShow);
            EmptyHeart.SetActive(!ShouldShow);
        }
    }

    void SetStamina(int stamina)
    {
        StaminaBar.value = stamina;
    }
}
