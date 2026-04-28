using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt = "Talk";
    [SerializeField] private string knotName;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        if (GameEventsManager.instance == null)
            return false;
        GameEventsManager.instance.dialogueEvents.EnterDialogue(knotName);
        return true;
    }
}
