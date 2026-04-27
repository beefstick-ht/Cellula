using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using System;
public class DialogueManager : MonoBehaviour
{
    private bool dialoguePlaying = false;

    [Header("Ink Story")]

    [SerializeField] private TextAsset inkJson;

    private Story story;

  

    private void Awake()
    {
        story = new Story(inkJson.text);
    }

    private void Start() 
    {
        if (GameEventsManager.instance == null)
        {
            Debug.LogError("GameEventsManager not found - make sure it exists in the scene");
            return;
        }
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
    }

    private void OnDestroy()  
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        }
    }

    private void SubmitPressed()
    {
        //if dialogue isnt playing, we never want to register input here
        if (!dialoguePlaying)
        {
            return;
        }
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

        //kick off the story
        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        if (story.canContinue)
        {
            string dialogueLine = story.Continue();
            GameEventsManager.instance.dialogueEvents.DisplayDialogue(dialogueLine);
        }
        else
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        dialoguePlaying = false;

        //inform other parts of our system we stopped dialogue
        GameEventsManager.instance.dialogueEvents.DialogueFinished();

        //let player move again
      //  GameEventsManager.instance.playerEvents.EnablePlayerMovement();

        //reset story state
        story.ResetState();
    }
}
