using UnityEngine;

public class QuestRewardNPC : QuestDialogueNPC
{
    [SerializeField] private string rewardItemID;
    [SerializeField] private bool giveRewardOnStart = false; // gives item the option to be given at start or end of quest

    public override void StartDialogue()
    {
        bool isCompletingQuest = !questData.isQuestComplete &&
            questData.hasAcceptedQuest &&
            Inventory.instance.HasItem(requiredItemID);

        //run logic from base script
        base.StartDialogue();

        //if the quest just completed during the base call, give reward
        if (isCompletingQuest && rewardItemID != "")
        {
            GiveReward();
        }
    }

    private void GiveReward()
    {
        Inventory.instance.AddItem(rewardItemID);
    }


}
