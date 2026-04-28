using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class QuestDialogueNPC : MonoBehaviour, IInteractable
{
    [Header("UI Components")]
    public TextMeshProUGUI text;
    public Image dialogueBox;
    public Camera dialogueCam; //dedicated camera when talking to an npc
    public GameObject player;

    [Header("Dialogue Settings")]
    public float textSpeed = 0.05f;
    private int index;
    private bool typing = false;
    private string[] activeLines;  //depending on where the quest is at, the string will produce the respective lines

    [Header("Quest Configuration")]
    public string requiredItemID = "";
    public bool hasAcceptedQuest = false;
    public bool isQuestComplete = false;

    [Header("NPC Lines")]
    [TextArea] public string[] introductionLines; // Talk for the first time
    [TextArea] public string[] waitingLines;      // Talk while quest is active
    [TextArea] public string[] completionLines;   // Talk when item is found
    [TextArea] public string[] postQuestLines;     // Talk after quest is finished

    public string InteractionPrompt => "Talk";

    void Start()
    {
        text.text = string.Empty;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && typing)
        {
            if (text.text == activeLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = activeLines[index];
            }
        }
    }

    // This is called by your Interactor script
    public bool Interact(Interactor interactor)
    {
        StartDialogue();
        return true;
    }

    public void StartDialogue()
    {
        // Determine which set of lines to play
        if (isQuestComplete)
        {
            activeLines = postQuestLines;
        }
        else if (hasAcceptedQuest)
        {
            // Check the inventory system
            if (Inventory.instance != null && Inventory.instance.HasItem(requiredItemID))
            {
                activeLines = completionLines;
                Inventory.instance.RemoveItem(requiredItemID);
                isQuestComplete = true;
            }
            else
            {
                activeLines = waitingLines;
            }
        }
        else
        {
            activeLines = introductionLines;
            hasAcceptedQuest = true;
        }

        index = 0;
        text.text = string.Empty;
        typing = true;
        
        dialogueCam.gameObject.SetActive(true);
        player.SetActive(false);
        
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {   
        //takes a full sentence and breaks it into an array of letters to type out one by one
        foreach (char c in activeLines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed); //the typing effect
        }
    }

    void NextLine()
    {
        if (index < activeLines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        typing = false;
        text.text = string.Empty;
        dialogueCam.gameObject.SetActive(false);
        player.SetActive(true);
    }
}