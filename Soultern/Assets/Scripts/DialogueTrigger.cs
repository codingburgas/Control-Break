using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager DialogueManager;

    [SerializeField]
    private Vector2 DialogueBoundaries;

    void Start()
    {
        DialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            DialogueManager.StartDialogue(DialogueBoundaries);
    }
}