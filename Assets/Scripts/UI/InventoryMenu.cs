using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUIManager : MonoBehaviour
{
    [Header("References")]
    public List<ItemData> allPossibleItems; //all items we could pick up
    public Image[] uiSlots; //stores the images
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI nameText;

    private int selectedIndex = 0;
    private List<ItemData> currentItemsInUI = new List<ItemData>();

    void OnEnable()
    {
        RefreshUI();
    }

    void Update()
    {
        if (currentItemsInUI.Count == 0) return;

 //cycle keys
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            ChangeSelection(1);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            ChangeSelection(-1);

        if (Input.GetKeyDown(KeyCode.E))
            EquipItem();
    }

    void RefreshUI()
    {
        currentItemsInUI.Clear();
        //check which items the player actually has
        foreach (string id in Inventory.instance.items)
        {
            ItemData data = allPossibleItems.Find(x => x.id == id);
            if (data != null) currentItemsInUI.Add(data);
        }

        //update the visual slots
        for (int i = 0; i < uiSlots.Length; i++)
        {
            if (i < currentItemsInUI.Count)
            {
                uiSlots[i].sprite = currentItemsInUI[i].icon;
                uiSlots[i].enabled = true;
            }
            else
            {
                uiSlots[i].enabled = false; //hide empty slots
            }
        }
        UpdateSelectionVisuals();
    }

    void ChangeSelection(int direction)
    {
        if (currentItemsInUI.Count == 0) return; //if empty do nothing
        selectedIndex = (selectedIndex + direction + currentItemsInUI.Count) % currentItemsInUI.Count;
        UpdateSelectionVisuals();
    }

    void UpdateSelectionVisuals()
    {
        // if there are no items, clear the text and stop
        if (currentItemsInUI.Count == 0)
        {
            nameText.text = "";
            descriptionText.text = "Inventory is empty...";
            // hide all slots
            for (int i = 0; i < uiSlots.Length; i++) uiSlots[i].enabled = false; 
            return;
        }

        //ensure selectedIndex hasnt gone out of bounds
        if (selectedIndex >= currentItemsInUI.Count) selectedIndex = 0;

        for (int i = 0; i < currentItemsInUI.Count; i++)
        {
            bool isSelected = (i == selectedIndex);
            uiSlots[i].sprite = isSelected ? currentItemsInUI[i].highlightedIcon : currentItemsInUI[i].icon;
            uiSlots[i].transform.localScale = isSelected ? Vector3.one * 1.2f : Vector3.one;
        }

        ItemData selectedItem = currentItemsInUI[selectedIndex];
        nameText.text = selectedItem.itemName;
        descriptionText.text = selectedItem.description;
    }

    void EquipItem()
    {
        if (currentItemsInUI[selectedIndex].isEquippable)
        {
            Debug.Log("Equipped: " + currentItemsInUI[selectedIndex].itemName);
            // add equipment logic here
        }
    }
}