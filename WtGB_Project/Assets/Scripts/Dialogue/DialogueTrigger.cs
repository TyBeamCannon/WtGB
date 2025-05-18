using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{

    [SerializeField] bool debugDialogueShow;

    public Dialogue dialogue;

    void TriggerDialogue()
    {
        GameManager.instance.StartDialogue(dialogue);
    }

    
}
