using UnityEngine;

public class KeyCollectible : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private Key key;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor) //can be used to check the player's inventory
    {
        var KeyInventory = interactor.GetComponent<Inventory>();

        if (KeyInventory == null)
            return false;

        if (key == null)
            return false;
        {
            KeyInventory.Instance.AddKey(key);

            gameObject.SetActive(false);
        }
    }
    

}