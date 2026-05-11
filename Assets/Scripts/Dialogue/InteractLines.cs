using NUnit.Framework.Interfaces;
using QuickOutline;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractLines : MonoBehaviour, IInteractable
{
    [Header("UI Components")]
    public TextMeshProUGUI text;
    public Image dialogueBox;
    public Camera dialogueCam; //dedicated camera when talking to an npc
 
    [Header("Dialogue Settings")]
    public float textSpeed = 0.05f;
    private int index;
    private bool typing = false;
    private string[] activeLines;  //depending on where the quest is at, the string will produce the respective lines
    public static bool IsInDialogue { get; private set; }
    private bool isLocked = false;//prevents reopening during cooldown


    [Header("NPC Lines")]
    [TextArea] public string[] interactionLines; // Talk for the first time

    //all of the stuff below is in regards to the outline interactable

    private QuickOutline.Outline outline;
    [SerializeField] private string npcName = "";  //have to pass in a name since interaface says this property must exist

    public string DisplayName => npcName;

    void Awake()
    {
        outline = gameObject.AddComponent<QuickOutline.Outline>();
        outline.OutlineMode = QuickOutline.Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
        outline.enabled = false;
    }

    public bool CanInteract() => !typing; //if alr talking cannot talk

    public void Interact() => StartDialogue();

    public void OnFocusGained()
    {
        outline.enabled = true;
    }

    public void OnFocusLost() => outline.enabled = false;


    void Start()
    {
        text.text = string.Empty;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && typing)
        {
            if (text.text == activeLines[index])
            {
                NextLine(); 
            }
            else
            {
                StopAllCoroutines();
                text.text = activeLines[index];
                // update the UI text immediately when skipping
                DialoguePanelUI.instance.UpdateText(text.text);
            }
        }
    }
    // this is called by Interactor script
    public bool Interact(Interactor interactor)
    {
        StartDialogue();
        return true;
    }

    public void StartDialogue()
    {
        IsInDialogue = true;
        //npc decides which lines to use
        if (IsInDialogue == true)
        {
            activeLines = interactionLines;
        }
        // turn on the panel through the dialoguepanelUI script
        DialoguePanelUI.instance.OpenPanel();

            index = 0;
            text.text = string.Empty;
            typing = true;

            // Swap cameras and freeze the player
            dialogueCam.gameObject.SetActive(true);
            

            StartCoroutine(TypeLine());

    }

        IEnumerator TypeLine()
        {
            //takes a full sentence and breaks it into an array of letters to type out one by one
            foreach (char c in activeLines[index].ToCharArray())
            {
                text.text += c;
                yield return new WaitForSeconds(textSpeed); //the typing effect
                DialoguePanelUI.instance.UpdateText(text.text);
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
            IsInDialogue = false;
            typing = false;
            text.text = string.Empty;
            DialoguePanelUI.instance.ClosePanel();
            dialogueCam.gameObject.SetActive(false);
        StartCoroutine(DialogueCooldown());

    }
    IEnumerator DialogueCooldown()
    {
        isLocked = true;
        // Keep IsInDialogue true so the Interactor ignores "E"
        IsInDialogue = true;

        // Wait for exactly 2 seconds
        yield return new WaitForSeconds(2f);

        isLocked = false;
        IsInDialogue = false;
    }
}
