using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [Header("UI Reference")]
    public GameObject inventoryMenu;
    public GameObject blurVolume; //blue
    private bool isMenuOpen = false;

    [Header("Item Tracking")]
    // list of strings representing the item IDs currently held
    public List<string> items = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // this object survives scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        inventoryMenu.SetActive(isMenuOpen);

        // turns the extra blurring camera on/off
        if (blurVolume != null)
        {
            blurVolume.SetActive(isMenuOpen);
        }

        Time.timeScale = isMenuOpen ? 0 : 1; //freeze time
    }

    public bool HasItem(string itemID)
    {
        return items.Contains(itemID);
    }

    // removes the item when the quest is finished
    public void RemoveItem(string itemID)
    {
        if (items.Contains(itemID))
        {
            items.Remove(itemID);
            Debug.Log("Removed item: " + itemID);
        }
    }
    public void AddItem(string id)
    {
        if (!items.Contains(id))
        {
            items.Add(id);
            Debug.Log($"Item {id} added to inventory.");
        }
    }
}