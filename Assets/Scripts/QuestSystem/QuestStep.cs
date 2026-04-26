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
            QuestEvents.onQuestStepFinished?.Invoke(questId);
            Destroy(gameObject);
        }
    }

    protected void UpdateQuestStepState(string state)
    {
        QuestEvents.onQuestStepStateChanged?.Invoke(questId, state);
    }
}
