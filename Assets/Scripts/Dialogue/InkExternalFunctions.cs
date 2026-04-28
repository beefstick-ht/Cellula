using UnityEngine;
using Ink.Runtime;
public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction ("StartQuest", (string questId) => StartQuest(questId));
    }

    public void Unbind(Story story)
    {

    }
   
    private void StartQuest(string questId)
    {
        GameEventsManager.instance.questEvents.StartQuest(questId);
    }

    private void AdvanceQuest(string questId)
    {
        GameEventsManager.instance.questEvents.AdvanceQuest(questId);
    }

    private void FinishQuest(string questId)
    {
        GameEventsManager.instance.questEvents.FinishQuest(questId);
    }
}
