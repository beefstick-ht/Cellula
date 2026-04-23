using UnityEngine;
//abstract meaning it is meant to be an inhereted class, not to be used directly
public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            // TO DO: advance the quest forward now that we've finished this step

            Destroy(this.gameObject);
        }
    }
}
