using UnityEngine;

public class CollectItemQuestStep : QuestStep
{
    [Header("Config")]
    [SerializeField] private string itemID;
    [SerializeField] private int targetAmount = 1;

    private int currentlyCollected = 0;

    private void OnEnable()
    {
        // You'll need an event for when items are picked up
        // GameEventsManager.instance.inventoryEvents.onItemCollected += OnItemCollected;
    }

    private void OnDisable()
    {
        // GameEventsManager.instance.inventoryEvents.onItemCollected -= OnItemCollected;
    }

    private void OnItemCollected(string collectedItemID)
    {
        if (collectedItemID == itemID)
        {
            currentlyCollected++;
            UpdateQuestStepState($"Collected {currentlyCollected} / {targetAmount} {itemID}");

            if (currentlyCollected >= targetAmount)
            {
                FinishQuestStep(); // This tells QuestManager to MoveToNextStep
            }
        }
    }
}