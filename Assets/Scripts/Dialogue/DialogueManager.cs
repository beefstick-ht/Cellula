using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using System;
public class DialogueManager : MonoBehaviour
{
    private bool dialoguePlaying = false;
    private int currentChoiceIndex = 0;
    private List<Choice> currentChoices = new List<Choice>();

    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJson;

    private Story story;

    private InkExternalFunctions inkExternalFunctions;

    private InkDialogueVariables inkDialogueVariables;

    private void Awake()
    {
        story = new Story(inkJson.text);
        inkExternalFunctions = new InkExternalFunctions();
        inkExternalFunctions.Bind(story);
        inkDialogueVariables = new InkDialogueVariables(story);
    }

    private void Start()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        GameEventsManager.instance.dialogueEvents.onChoiceIndexUpdated += OnChoiceIndexUpdated;
        GameEventsManager.instance.dialogueEvents.onSubmitPressed += OnSubmitPressed;
        GameEventsManager.instance.dialogueEvents.onNavigateChoice += OnNavigateChoice;
        GameEventsManager.instance.dialogueEvents.onUpdateInkDialogueVariable += UpdateInkDialogueVariable;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    private void OnDestroy()
    {
        inkExternalFunctions.Unbind(story);

        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
            GameEventsManager.instance.dialogueEvents.onChoiceIndexUpdated -= OnChoiceIndexUpdated;
            GameEventsManager.instance.dialogueEvents.onSubmitPressed -= OnSubmitPressed;
            GameEventsManager.instance.dialogueEvents.onNavigateChoice -= OnNavigateChoice;
            GameEventsManager.instance.dialogueEvents.onUpdateInkDialogueVariable -= UpdateInkDialogueVariable;
            GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        }
    }
    private void QuestStateChange(Quest quest)
        {
        GameEventsManager.instance.dialogueEvents.UpdateInkDialogueVariable(quest.info.id + "State",(Ink.Runtime.Object) new StringValue(quest.state.ToString()));
    }

    private void UpdateInkDialogueVariable(string name, Ink.Runtime.Object value)
    {
        inkDialogueVariables.UpdateVariableState(name, value);
    }
    private void OnSubmitPressed()
    {
        if (!dialoguePlaying)
            return;

        if (currentChoices.Count > 0)
        {
            MakeChoice(currentChoiceIndex); // confirm selected choice
        }
        else
        {
            ContinueOrExitStory(); // no choices, just continue
        }
    }

    private void OnChoiceIndexUpdated(int index)
    {
        currentChoiceIndex = index;
        Debug.Log("Manager updated: currentChoiceIndex is now " + index);
    }

    private void MakeChoice(int choiceIndex)
    {
        Debug.Log("INK ACTION: Choosing index " + choiceIndex);
        story.ChooseChoiceIndex(choiceIndex);
        currentChoices.Clear();
        ContinueOrExitStory();
    }

    private void EnterDialogue(string knotName)
    {
        if (dialoguePlaying) //dont enter dialogue if alr have
        {
            return;
        }

        dialoguePlaying = true;


        //inform other parts of our system we started dialogue
        GameEventsManager.instance.dialogueEvents.DialogueStarted();

        //freeze player movements
        //GameEventsManager.instance.playerEvents.DisablePlayerMovement();

        //jump to the knot
        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
        }
        else
        {
            Debug.LogWarning("Knot name was the empty string when entering dialogue.");
        }

        //start listening for variables
        inkDialogueVariables.SyncVariableAndStartListening(story);

        //kick off the story
        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        if (story.canContinue)
        {
            string dialogueLine = story.Continue();
            currentChoices = story.currentChoices;
            Debug.Log("Dialogue line: " + dialogueLine);
            foreach (Choice choice in currentChoices)
            {
                Debug.Log("Choice " + choice.index + ": " + choice.text);
            }

            GameEventsManager.instance.dialogueEvents
                .DisplayDialogue(dialogueLine, currentChoices);
        }
        else if (story.currentChoices.Count > 0)
        {
            currentChoices = story.currentChoices;

            foreach (Choice choice in currentChoices)
            {
                Debug.Log("Choice " + choice.index + ": " + choice.text);
            }

            GameEventsManager.instance.dialogueEvents
                .DisplayDialogue("", currentChoices);
        }
        else
        {
            ExitDialogue();
        }
    }


    private void ExitDialogue()
    {
        dialoguePlaying = false;
        currentChoices.Clear();
        //inform other parts of our system we stopped dialogue
        GameEventsManager.instance.dialogueEvents.DialogueFinished();

        //let player move again
        //  GameEventsManager.instance.playerEvents.EnablePlayerMovement();

        //stop listening for dialogue variables
        inkDialogueVariables.StopListening(story);

        //reset story state
        story.ResetState();
    }

    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }

    private void OnNavigateChoice(int direction)
    {
        if (currentChoices.Count == 0) return;

        int newIndex = Mathf.Clamp(currentChoiceIndex + direction, 0, currentChoices.Count - 1);

        // Tell the system the index updated
        GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(newIndex);

        // Tell the UI to visually move the selection
        GameEventsManager.instance.dialogueEvents.SelectionChanged(newIndex);
    }
}
