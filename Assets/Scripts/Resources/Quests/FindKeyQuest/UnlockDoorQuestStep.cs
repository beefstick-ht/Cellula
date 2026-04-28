using UnityEngine;

public class UnlockDoorQuestStep : QuestStep
{
    [SerializeField] private string doorID;

    private void OnEnable()
    {
        DoorEvents.onDoorOpened += OnDoorOpened;
    }

    private void OnDisable()
    {
        DoorEvents.onDoorOpened -= OnDoorOpened;
    }

    private void OnDoorOpened(string openedDoorID)
    {
        if (openedDoorID == doorID)
        {
            UpdateQuestStepState("Door unlocked");
            FinishQuestStep();
        }
    }
}
