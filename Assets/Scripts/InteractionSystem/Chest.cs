using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor) //can be used to check the player's inventory
    {
        Debug.Log("Opening Chest");
        return true;
    }
}
