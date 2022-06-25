using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartGenerator : MonoBehaviour
{
    public GameObject HeartPrefab;

    [HideInInspector] public float HeartSpeed = 0.1f;
    [HideInInspector] public float HeartRange = 1f; 

    private int HeartMinDelay;
    private int HeartMaxDelay;

    void Start()
    {
        HeartSpeed = Random.Range(0.125f, 0.326f);
        HeartRange = Random.Range(1.75f, 2.6f);

        HeartMinDelay = Random.Range(20, 41);
        HeartMaxDelay = Random.Range(40, 71);

        StartCoroutine(GenerateHeart());
    }

    IEnumerator GenerateHeart()
    {
        yield return new WaitForSecondsRealtime(Random.Range(HeartMinDelay, HeartMaxDelay));
        Instantiate(HeartPrefab, transform.position, Quaternion.identity);
        StartCoroutine(GenerateHeart());
    }
}