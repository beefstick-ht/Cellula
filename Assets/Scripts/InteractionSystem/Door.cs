using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
   
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor) //can be used to check the player's inventory
    {
        Debug.Log("Opening Door");
        return true;
    }
}
