using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinController : MonoBehaviour
{
    private Animator PumpkinAnimator;

    [HideInInspector] public bool IsDead = false;

    void Start()
    {
        PumpkinAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (IsDead)
            PumpkinAnimator.SetBool("IsDead", true);
    }
}