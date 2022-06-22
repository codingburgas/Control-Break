using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodsmanController : MonoBehaviour
{
    private ExtraFunctions ExtraFunctions;

    private Transform Woodsman;
    private Transform Player;

    public float Distance;

    [HideInInspector] public bool WillThrowAxe;
    
    void Start()
    {
        ExtraFunctions = GameObject.Find("ExtraFunctions").GetComponent<ExtraFunctions>();

        Woodsman = GameObject.Find("Woodsman").transform;
        Player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Distance = Mathf.Abs(Woodsman.position.x - Player.position.x);

        if (Distance <= 7.5f)
            ThrowAxe();
    }

    void ThrowAxe()
    {
        WillThrowAxe = true;
    }
}
