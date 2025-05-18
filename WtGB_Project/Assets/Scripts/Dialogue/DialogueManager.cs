using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] Animator animator;

    [SerializeField] Text nameText;
    [SerializeField] Text dialogueText;

    [SerializeField] float typingSpeed;

    Queue<DialogueLine> dialogueLines;

    bool isDialogueActive;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;

        animator.Play("Show");

        dialogueLines.Clear();

        foreach (DialogueLine line in dialogue.dialogueLines)
        {
            dialogueLines.Enqueue(line);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (dialogueLines.Count == 0)
        {
            GameManager.instance.EndDialogue();
            return;
        }

        DialogueLine currLine = dialogueLines.Dequeue();

        nameText.text = currLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueText.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("Hide");
    }

    public bool DialogueIsActive => isDialogueActive;
}
