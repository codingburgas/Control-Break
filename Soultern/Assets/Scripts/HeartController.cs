using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    private HeartGenerator HeartGenerator;

    private float BeginningY;

    void Start()
    {
        HeartGenerator = GameObject.Find("HeartGenerator").GetComponent<HeartGenerator>();

        BeginningY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x + 0.009f, transform.position.y, transform.position.z);

        float y = Mathf.PingPong(Time.time * HeartGenerator.HeartSpeed, 1) * HeartGenerator.HeartRange;

        transform.position = new Vector3(transform.position.x, BeginningY + y, transform.position.z);
        
        if (transform.position.x >= 40)
            Destroy(gameObject);
    }
}
