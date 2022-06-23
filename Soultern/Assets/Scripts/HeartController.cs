using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    private HeartGenerator HeartGenerator;
    private MenuManager MenuManager;

    private float BeginningY;

    void Start()
    {
        HeartGenerator = GameObject.Find("HeartGenerator").GetComponent<HeartGenerator>();
        MenuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();

        BeginningY = transform.position.y;
    }

    void Update()
    {
        if (MenuManager.IsPaused || MenuManager.IsGameOver) return;
        transform.position = new Vector3(transform.position.x + 0.009f, transform.position.y, transform.position.z);

        float y = Mathf.PingPong(Time.time * HeartGenerator.HeartSpeed, 1) * HeartGenerator.HeartRange;

        transform.position = new Vector3(transform.position.x, BeginningY + y, transform.position.z);
        
        if (transform.position.x >= 40)
            Destroy(gameObject);
    }
}
