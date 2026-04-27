using UnityEngine;

public class QuestGiver : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestInfoSO quest;
    [SerializeField] private string prompt = "Talk";

    public string InteractionPrompt => prompt;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += OnQuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= OnQuestStateChange;
    }

    private void OnQuestStateChange(Quest quest)
    {
        // update prompt based on quest state
        if (quest.info.id == this.quest.id)
        {
            if (quest.state == QuestState.CAN_START)
                prompt = "Talk"; // can accept quest
            else if (quest.state == QuestState.IN_PROGRESS)
                prompt = "Talk"; // quest in progress
            else if (quest.state == QuestState.CAN_FINISH)
                prompt = "Talk"; // can turn in quest
        }
    }

    public bool Interact(Interactor interactor)
    {
        // get current quest state
        Quest currentQuest = GameEventsManager.instance
            .questEvents.GetQuest(quest.id);

        if (currentQuest == null) return false;

        if (currentQuest.state == QuestState.CAN_START)
        {
            // start the quest and play accept dialogue
            GameEventsManager.instance.questEvents.StartQuest(quest.id);
            // trigger dialogue when you build it:
            // GameEventsManager.instance.dialogueEvents
            //     .StartDialogue(quest.acceptDialogueID);
            return true;
        }

        if (currentQuest.state == QuestState.CAN_FINISH)
        {
            // finish the quest and play complete dialogue
            GameEventsManager.instance.questEvents.FinishQuest(quest.id);
            // trigger dialogue when you build it:
            // GameEventsManager.instance.dialogueEvents
            //     .StartDialogue(quest.completeDialogueID);
            return true;
        }

        if (currentQuest.state == QuestState.IN_PROGRESS)
        {
            // play in progress dialogue when you build it
            // GameEventsManager.instance.dialogueEvents
            //     .StartDialogue(quest.inProgressDialogueID);
            return true;
        }

        return false;
    }
}