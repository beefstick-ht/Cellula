using UnityEngine;

public class KeyCollectible : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private Key key;

    public string InteractionPrompt => prompt;
    private void Start()
    {
        // if player already has this key, hide the pickup
        if (KeyInventory.Instance != null && KeyInventory.Instance.HasKey(key))
        {
            gameObject.SetActive(false);
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (key == null)
            return false;

        KeyInventory inventory = KeyInventory.Instance; //refrencing keyinventory singleton, any script can then access it

        //finds the KeyInventory that exists, if it doesnt exist, will stop, if exists, will call AddKey

        if (inventory == null)
            return false;

        inventory.AddKey(key);
        KeyEvents.onKeyCollected?.Invoke(key);
        gameObject.SetActive(false); //makes the object appear like it was obtained
        return true;
    }

}