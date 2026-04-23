using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
   
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor) //can be used to check the player's inventory
    {
        var KeyInventory = interactor.GetComponent<Inventory>();

        if (KeyInventory == null)
            return false;

      //  if (inventory.hasKey)
        {
            Debug.Log("Opening Door");
            return true;
        }

        Debug.Log("No key found");
        return false;
    }
}
