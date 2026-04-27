using UnityEngine;
using System.Collections;

public class DialogueInputHandler : MonoBehaviour
{
    private bool dialogueIsPlaying = false;
    private bool inputEnabled = false;

    private void Start()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted += OnDialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished += OnDialogueFinished;
    }

    private void OnDestroy()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.dialogueEvents.onDialogueStarted -= OnDialogueStarted;
            GameEventsManager.instance.dialogueEvents.onDialogueFinished -= OnDialogueFinished;
        }
    }

    private void OnDialogueStarted()
    {
        dialogueIsPlaying = true;
        inputEnabled = false;
        // wait a moment before accepting input so the opening E press doesnt count
        StartCoroutine(EnableInputAfterDelay());
    }

    private void OnDialogueFinished()
    {
        dialogueIsPlaying = false;
        inputEnabled = false;
    }

    private IEnumerator EnableInputAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        inputEnabled = true;
    }

    private void Update()
    {
        if (!dialogueIsPlaying || !inputEnabled)
            return;

        // navigate choices with arrow keys
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameEventsManager.instance.dialogueEvents.NavigateChoice(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameEventsManager.instance.dialogueEvents.NavigateChoice(1);
        }
        // confirm with E
        else if (Input.GetKeyDown(KeyCode.E))
        {
            GameEventsManager.instance.dialogueEvents.SubmitPressed();
        }
    }
}