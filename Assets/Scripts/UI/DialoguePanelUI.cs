using TMPro;
using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using System;

public class DialoguePanelUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueChoiceButton[] choiceButtons;

    private void Awake()
    {
        contentParent.SetActive(false);
        ResetPanel();
    }

    private void Start()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted += DialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished += DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;
        GameEventsManager.instance.dialogueEvents.onSelectionChanged += UpdateVisualSelection;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted -= DialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished -= DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
    }

    private void DialogueStarted()
    {
        contentParent.SetActive(true);
    }

    private void DialogueFinished()
    {
        contentParent.SetActive(false);

        //reset anything for next time
        ResetPanel();
    }

    private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        dialogueText.text = dialogueLine;

        //  Hide all buttons first
        foreach (DialogueChoiceButton choiceButton in choiceButtons)
        {
            choiceButton.gameObject.SetActive(false);
        }

        // Fill buttons 1-to-1 (Top button = First choice)
        for (int i = 0; i < dialogueChoices.Count; i++)
        {
            if (i >= choiceButtons.Length) break;

            choiceButtons[i].gameObject.SetActive(true);
            choiceButtons[i].SetChoiceText(dialogueChoices[i].text);

            choiceButtons[i].SetChoiceIndex(dialogueChoices[i].index);

            // Auto-select the first one if we just started showing choices
            if (i == 0)
            {
                choiceButtons[i].SelectButton();
                GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(dialogueChoices[i].index);
            }
        }
    }

    private void ResetPanel()
    {
        dialogueText.text = "";
    }

    public event Action<int> onSelectionChanged;
    public void SelectionChanged(int index)
    {
        onSelectionChanged?.Invoke(index);
    }
    private void UpdateVisualSelection(int index)
    {
        if (index >= 0 && index < choiceButtons.Length)
        {
            choiceButtons[index].SelectButton();
        }
    }
}
