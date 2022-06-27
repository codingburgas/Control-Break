using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string Speaker;
    public string Text;
}

[System.Serializable]
public class AllDialogues
{
    public Dialogue[] Dialogues;
}

public class JSONReader : MonoBehaviour
{
    private TextAsset JsonFile;

    private AllDialogues DialoguesInJSON;

    void Start()
    {
        JsonFile = Resources.Load<TextAsset>("DialogueList");

        DialoguesInJSON = JsonUtility.FromJson<AllDialogues>(JsonFile.text);
    }

    public List<string> ReadJSON(Vector2 DialogueBoundaries)
    {
        List<string> Result = new List<string>();

        for (int i = (int)DialogueBoundaries.x; i <= (int)DialogueBoundaries.y; i++)
        {
            Result.Add(DialoguesInJSON.Dialogues[i].Speaker);
            Result.Add(DialoguesInJSON.Dialogues[i].Text);
        }

        for (int i = 0; i < 2; i++)
            Result.Add("IfThisShowsUpSomethingIsWrong");
        
        return Result;
    }
}