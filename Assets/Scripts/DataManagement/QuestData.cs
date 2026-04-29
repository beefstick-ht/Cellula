using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Systems/Quest Data")]
public class QuestData : ScriptableObject
{
    public bool hasAcceptedQuest;
    public bool isQuestComplete;
    public void ResetQuest()
    {
        hasAcceptedQuest = false;
        isQuestComplete = false;
    }
}