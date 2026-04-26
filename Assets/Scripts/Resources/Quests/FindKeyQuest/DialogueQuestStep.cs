using UnityEngine;

public class DialogueQuestStep : MonoBehaviour
{
    [SerializeField] private string npcID;

    private void OnEnable()
    {
        DialogueEvents.onDialogueFinished += OnDialogueFinished;
    }

    private void OnDisable()
    {
        DialogueEvents.onDialogueFinished -= OnDialogueFinished;
    }

    private void OnDialogueFinished(string finishedNpcID)
    {
        if (finishedNpcID == npcID)
        {
            UpdateQuestStepState("Talked to NPC");
            FinishQuestStep();
        }
    }
}
