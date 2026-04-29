using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isLocked = false;
    [SerializeField] private string requiredItemID = ""; //specific key used to unlock door using id
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private LevelTransition levelTransition;
    [SerializeField] private string doorID;  //specific doors have specific keys

    [Header("Prompts")]
    [SerializeField] private string lockedPrompt = "The door is locked.";
    [SerializeField] private string unlockedPrompt = "Use Key?";
    [SerializeField] private string openPrompt = "Opening door...";

    private bool isOpen = false;

    public string InteractionPrompt
    {
        get
        {
            if (isOpen) return openPrompt;

            if (isLocked)
            {
                // Check if the player has the string ID in their inventory
                if (Inventory.instance != null && Inventory.instance.HasItem(requiredItemID))
                {
                    return unlockedPrompt;
                }
                return lockedPrompt;
            }

            return "Open Door";
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (isOpen) return false;

        if (isLocked)
        {
            // Check the inventory for the string ID
            if (Inventory.instance != null && Inventory.instance.HasItem(requiredItemID))
            {
                OpenDoor();
                return true;
            }

            Debug.Log("Door is locked. You need: " + requiredItemID);
            return false;
        }

        // If not locked, just open
        OpenDoor();
        return true;
    }

    private void OpenDoor()
    {
        isOpen = true;

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }

        // Disable collider so player can walk through
        if (GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = false;
        }

        // Trigger level transition if it exists
        if (levelTransition != null)
        {
            levelTransition.enabled = true;
        }
    }
}