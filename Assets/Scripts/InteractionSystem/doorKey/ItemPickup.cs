using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt = "";
    [SerializeField] private string itemID; 

    public string InteractionPrompt => prompt;
    private void Start()
    {
        // Check if player already has this specific item
        if (Inventory.instance != null && Inventory.instance.HasItem(itemID))
        {
            // If they have it this object is set inactive
            gameObject.SetActive(false);
        }
    }
    public bool Interact(Interactor interactor)
    {
        if (Inventory.instance == null)
        {
            Debug.LogError("No Inventory instance found in the scene");
            return false;
        }
        Inventory.instance.AddItem(itemID);

        //Trigger a generic event if you still want sound/UI effects
        // GameEventsManager.instance.onItemCollected?.Invoke(itemID);

        gameObject.SetActive(false);

        return true;
    }

}