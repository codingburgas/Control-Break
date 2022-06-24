using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinController : MonoBehaviour
{
    private Animator PumpkinAnimator;
    
    [HideInInspector] public AudioSource PumpkinDeath;
    [HideInInspector] public AudioSource DamageSound;
    [HideInInspector] public bool IsDead = false;

    void Start()
    {
        PumpkinAnimator = this.GetComponent<Animator>();
        
        PumpkinDeath = this.gameObject.transform.GetChild(0).GetComponent<AudioSource>();
        DamageSound = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (IsDead)
        {
            PumpkinAnimator.SetBool("IsDead", true);

            Fade(0.4f);
        }
    }

    void Fade(float FadeSpeed)
    {
        Color PumpkinColor = this.GetComponent<SpriteRenderer>().color;

        PumpkinColor.a -= Time.deltaTime * FadeSpeed;

        this.GetComponent<SpriteRenderer>().color = PumpkinColor;
        transform.position = new Vector3(transform.position.x, transform.position.y - FadeSpeed / 250, transform.position.z);
    }
}