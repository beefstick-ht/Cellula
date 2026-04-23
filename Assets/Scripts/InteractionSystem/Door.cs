using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
   
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor) //can be used to check the player's inventory
    {
        var inventory = interactor.GetComponent<Inventory>();

        if (inventory == null)
            return false;

        if (inventory.HasKey)
        {
            Debug.Log("Opening Door");
            return true;
        }

        Debug.Log("No key found");
        return false;
    }
}
