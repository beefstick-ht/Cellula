using UnityEngine;

public interface IInteractable //is what is connecting all the other scripts to the interact function
{
    Transform transform { get; }

    string DisplayName { get; }

    bool CanInteract();
    void Interact();
    void OnFocusGained();
    void OnFocusLost();

}
