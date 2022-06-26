using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private JSONReader JSONReader;
    private ExtraFunctions ExtraFunctions;

    private GameObject DialogueBox;
    private GameObject DialogueName;
    private GameObject DialogueText;

    private List<string> Dialogue;

    [HideInInspector] public List<string> Speakers;
    [HideInInspector] public List<string> Texts;

    private int Current;

    [HideInInspector] public bool IsInDialogue = false;

    void Start()
    {
        JSONReader = GameObject.Find("JSONReader").GetComponent<JSONReader>();
        ExtraFunctions = GameObject.Find("ExtraFunctions").GetComponent<ExtraFunctions>();

        DialogueBox = ExtraFunctions.FindInactive("Dialogue Box");
        DialogueName = DialogueBox.transform.Find("Name").gameObject;
        DialogueText = DialogueBox.transform.Find("Text").gameObject;
    }

    void ReadDialogue(Vector2 DialogueBoundaries)
    {
        Dialogue = JSONReader.ReadJSON(DialogueBoundaries);

        for (int i = 0; i < Dialogue.Count; i += 2)
        {
            Speakers.Add(Dialogue[i]);
            Texts.Add(Dialogue[i + 1]);
        }
    }

    void CheckIfEnded()
    {
        if (Current == Texts.Count)
        {
            DialogueBox.SetActive(false);
            Time.timeScale = 1f;

            Speakers.Clear();
            Texts.Clear();

            IsInDialogue = false;
        }
    }

    public void NextSentence()
    {
        DialogueName.GetComponent<Text>().text = Speakers[Current];
        DialogueText.GetComponent<Text>().text = Texts[Current];
        Current++;
        CheckIfEnded();
    }

    public void StartDialogue(Vector2 DialogueBoundaries)
    {
        IsInDialogue = true;
        
        DialogueBoundaries -= new Vector2(1, 1);
        ReadDialogue(DialogueBoundaries);

        DialogueBox.SetActive(true);

        Current = 0;
        NextSentence();
    }
}
