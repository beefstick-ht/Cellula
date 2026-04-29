using UnityEngine;

public class DialogueQuestStep : QuestStep
{
    [SerializeField] private string npcID;

    private void OnEnable()
    {
        // uncomment when dialogue system is built:
        // DialogueEvents.onDialogueFinished += OnDialogueFinished;
    }

    private void OnDisable()
    {

        // uncomment when dialogue system is built:
        // DialogueEvents.onDialogueFinished -= OnDialogueFinished;
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