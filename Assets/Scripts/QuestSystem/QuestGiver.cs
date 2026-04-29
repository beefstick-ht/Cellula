using UnityEngine;

public class QuestGiver : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestInfoSO quest;
    [SerializeField] private string prompt = "Talk";

    [Header("Dialogue Knots")]
    [SerializeField] private string startKnot;        // before quest starts
    [SerializeField] private string inProgressKnot;   // while quest is active
    [SerializeField] private string finishKnot;     // when quest can finish

    public string InteractionPrompt => prompt;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += OnQuestStateChange;
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.questEvents.onQuestStateChange -= OnQuestStateChange;
        }
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
        if (GameEventsManager.instance == null) return false;

        Quest currentQuest = GameEventsManager.instance.questEvents.GetQuest(quest.id);
        if (currentQuest == null) return false;

        // Use a string to hold which knot we should play
        string knotToPlay = "";

        if (currentQuest.state == QuestState.CAN_START)
        {
            knotToPlay = startKnot;
            GameEventsManager.instance.questEvents.StartQuest(quest.id);
        }
        else if (currentQuest.state == QuestState.CAN_FINISH)
        {
            knotToPlay = finishKnot;
            GameEventsManager.instance.questEvents.FinishQuest(quest.id);
        }
        else if (currentQuest.state == QuestState.IN_PROGRESS)
        {
            knotToPlay = inProgressKnot;
        }

        // This is the trigger that makes the DialogueUI appear
        if (!string.IsNullOrEmpty(knotToPlay))
        {
            GameEventsManager.instance.dialogueEvents.EnterDialogue(knotToPlay);
            return true;
        }

        return false;
    }
}