using UnityEngine;

public class CollectKeyQuestStep : QuestStep
{
 [SerializeField] private Key requiredKey;

    private void OnEnable()
    {
        // listen for when any key is collected
        KeyEvents.onKeyCollected += OnKeyCollected;
    }

    private void OnDisable()
    {
        KeyEvents.onKeyCollected -= OnKeyCollected;
    }

    private void OnKeyCollected(Key key)
    {
        if (key.id == requiredKey.id)
        {
            UpdateQuestStepState("Key collected");
            FinishQuestStep();
        }
    }
}
