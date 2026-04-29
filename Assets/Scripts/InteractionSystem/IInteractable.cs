using UnityEngine;

public interface IInteractable //is what is connecting all the other scripts to the interact function
{
    public string InteractionPrompt { get; }

    public bool Interact (Interactor interactor);
}
