using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;  //taking the position of the interaction point
    [SerializeField] private float interactionPointRadius = 0.5f;  //how large the range is for the player to interact
    [SerializeField] private LayerMask interactableMask;  //what shows up when in range
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    private IInteractable interactable;

    private bool isDisabled = false;
    private float reEnableDelay = 0.2f; //delay so the dialogue can close 

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        GameEventsManager.instance.dialogueEvents.onDialogueStarted += OnDialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished += OnDialogueFinished;
    }

    private void OnDestroy()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.dialogueEvents.onDialogueStarted -= OnDialogueStarted;
            GameEventsManager.instance.dialogueEvents.onDialogueFinished -= OnDialogueFinished;
        }
    }
    private void OnDialogueStarted()
    {
        isDisabled = true;
        // hide prompt if showing
        playerController.canMove = false;
        if (interactionPromptUI.isDisplayed)
            interactionPromptUI.Close();
    }

    private void OnDialogueFinished()
    {
        playerController.canMove = true;
        StartCoroutine(ReEnableAfterDelay());
    }

    private void Update()
    {
        if (isDisabled) return; // skip everything if dialogue is playing

        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, 
            interactionPointRadius, colliders, interactableMask);  //counts how many interaction points are in radius

        if (numFound > 0)
        { //if there is an interactable object in front of the player and you have pressed the E key, then we can interact since we are the interactor interacting (lol)
             interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!interactionPromptUI.isDisplayed)
                    interactionPromptUI.Setup(interactable.InteractionPrompt);

                if (Input.GetKeyDown(KeyCode.E))
                    interactable.Interact(this);
            }
        }
        else
        {
         if (interactable != null)   interactable = null;
            if (interactionPromptUI.isDisplayed) interactionPromptUI.Close();
        }
    }
    private IEnumerator ReEnableAfterDelay()
    {
        // wait for input to clear
        yield return new WaitForSeconds(0.3f);
        isDisabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
