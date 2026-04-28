using Ink.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvents
{
    public event Action<int> onChoiceIndexUpdated;

    public event Action<string> onEnterDialogue;
    public event Action<int> onSelectionChanged;

    public void SelectionChanged(int index)
    {
        onSelectionChanged?.Invoke(index);
    }

    public void EnterDialogue(string knotName)
    {
        onEnterDialogue?.Invoke(knotName);
    }

    public event Action onDialogueStarted;
    public void DialogueStarted()
    {
        onDialogueStarted?.Invoke();
    }

    public event Action onDialogueFinished;
    public void DialogueFinished()
    {
        onDialogueFinished?.Invoke();
    }

    public event Action<string, List<Choice>> onDisplayDialogue;
    public void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        onDisplayDialogue?.Invoke(dialogueLine, dialogueChoices);
    }

    public void UpdateChoiceIndex(int choiceIndex)
    {
        if (onChoiceIndexUpdated != null)
        {
            onChoiceIndexUpdated(choiceIndex);
        }
    }

    public event Action onSubmitPressed;
    public void SubmitPressed()
    {
        onSubmitPressed?.Invoke();
    }

    public event Action<int> onNavigateChoice;
    public void NavigateChoice(int direction)
    {
        onNavigateChoice?.Invoke(direction);
    }

    public event Action<string, Ink.Runtime.Object> onUpdateInkDialogueVariable;
    public void UpdateInkDialogueVariable(string name, Ink.Runtime.Object value)
    {
        if (onUpdateInkDialogueVariable != null)
        {
            onUpdateInkDialogueVariable(name, value);
        }
    }
}