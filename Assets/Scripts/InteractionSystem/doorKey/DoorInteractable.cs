using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isLocked = false; //allows for some doors to be locked while others can stay unlocked
    [SerializeField] private Key requiredKey;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private LevelTransition levelTransition;

    [Header("Prompts")]
    [SerializeField] private string lockedPrompt = "The door is locked.";
    [SerializeField] private string unlockedPrompt = "Use Key?";
    [SerializeField] private string openPrompt = "Opening door.";

    private bool isOpen = false;

    public string InteractionPrompt
    {
        get
        {
            if (isOpen)
                return openPrompt;

            if (KeyInventory.Instance != null && KeyInventory.Instance.HasKey(requiredKey))
                return unlockedPrompt;

            if(isLocked)
                return lockedPrompt;

            return unlockedPrompt; //unlocked doors always show open prompt
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (isOpen)
            return false;

        if (isLocked)
        {

            if (requiredKey == null)
            {
                Debug.LogError("No key assigned to this door");
                return false;
            }

            if (KeyInventory.Instance == null)
            {
                Debug.LogError("No KeyInventory found");
                return false;
            }

            if (KeyInventory.Instance.HasKey(requiredKey))
            {
                isOpen = true;
                doorAnimator?.SetTrigger("Open");
                GetComponent<Collider>().enabled = false;
                return true;
            }

            return false;
        }
        //either the door is alr unlocked or the player HasKey

        OpenDoor();
        return true;
    }

    private void OpenDoor()
    {
        isOpen = true;
        doorAnimator?.SetTrigger("Open");
        DoorEvents.onDoorOpened?.Invoke(doorID);
        if (levelTransition != null)
            levelTransition.enabled = true;
    }
  
}