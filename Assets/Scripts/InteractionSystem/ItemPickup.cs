using UnityEngine;
using QuickOutline;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemID;
    [SerializeField] private string itemName = "";
    public QuestData questData;

    public string DisplayName => itemName;
    public string InteractionPrompt => "Pick up " + itemName;
    private Outline outline;

    private void Awake()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
        outline.enabled = false;
    }

    private void Start()
    {
        // of this items id is marked as complete in our data asset, vanish immediately
        if (questData.isQuestComplete && itemID == "YourSpecificItemID")
        {
            gameObject.SetActive(false);
        }
    }

public bool CanInteract() => true;

    public void Interact()
    {
        if (Inventory.instance == null)
        {
            Debug.LogError("No Inventory instance found in the scene.");
            return;
        }

        Inventory.instance.AddItem(itemID);

        // hide the item after picking it up
        gameObject.SetActive(false);
    }

    public void OnFocusGained()
    {
        if (outline != null) outline.enabled = true;
    }

    public void OnFocusLost()
    {
        if (outline != null) outline.enabled = false;
    }
}

