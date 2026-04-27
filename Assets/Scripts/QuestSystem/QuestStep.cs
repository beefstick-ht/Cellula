using UnityEngine;
//abstract meaning it is meant to be an inhereted class, not to be used directly
public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    private string questId;

    public void InitializeQuestStep(string questId)
    {
        this.questId = questId;
    }
    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            GameEventsManager.instance.questEvents.AdvanceQuest(questId);
            Destroy(this.gameObject);
        }
    }
    protected void UpdateQuestStepState(string state)
    {
        Debug.Log("Quest step state: " + state);
        // later you can hook this into UI or dialogue
    }
}
