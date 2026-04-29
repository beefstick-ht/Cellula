using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // This allows the NPC to say: Inventory.instance.HasItem(...)
    public static Inventory instance;

    [Header("UI Reference")]
    public GameObject inventoryMenu;
    private bool isMenuOpen = false;

    [Header("Item Tracking")]
    // A list of strings representing the item IDs currently held
    public List<string> items = new List<string>();

    void Awake()
    {
        if (instance == null) instance = this;
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

     //freezes time when menu is open
        Time.timeScale = isMenuOpen ? 0 : 1;
        Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
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