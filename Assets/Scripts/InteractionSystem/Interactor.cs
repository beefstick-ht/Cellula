using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.Collections;


public class Interactor : MonoBehaviour
{
    [SerializeField] private float radius = 2f;
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private InteractionPrompt prompt;
    private Collider[] buffer = new Collider[32]; //contains all the objects around player
    private IInteractable focused;  //object currently focusing
    private void Awake()
    {
        // If the slot is empty, find the UI in the scene automatically
        if (prompt == null)
        {
            prompt = FindFirstObjectByType<InteractionPrompt>();
        }
    }
    private void Update()
    {
        // if npc is currently talking, dont allow new interactions
        if (QuestDialogueNPC.IsInDialogue) return;

        IInteractable nearest = FindNearestInteractable();
        UpdateFocus(nearest);

        if (focused != null && Input.GetKeyDown(KeyCode.E))
        {
            if (focused.CanInteract()) focused.Interact();
        }
    }

    private IInteractable FindNearestInteractable()
    {
        //finds all the objects around the player
        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, buffer, interactableLayers, QueryTriggerInteraction.Collide);
        IInteractable nearest = null;
        float bestDistSq = float.MaxValue;

        for (int i = 0; i < count; i++)//goes through each collider that is not null and try to get an Interactable that player can interact with
        {
            Collider col = buffer[i];
            if (col == null) continue;
            IInteractable interactable = col.GetComponentInParent<IInteractable>();
            if (interactable == null) continue;
            //if we have one then we check if its distance to the player is smaller than the current nearest
            if (!interactable.CanInteract()) continue;
            //if it is we can set it as the nearest interactable
            float distSq = (col.transform.position - transform.position).sqrMagnitude;    
            
            if(distSq < bestDistSq)
            {
                bestDistSq = distSq;
                nearest = interactable;
            }
        }
        return nearest;
    }    
    //checks if the new interactable is not closer than the current
    private void UpdateFocus(IInteractable nearest)
    {
        if (ReferenceEquals(focused, nearest)) return;
        focused?.OnFocusLost();
        focused = nearest;
        if(focused!= null)
        {
            focused.OnFocusGained();
            prompt.Show(focused);
        }
        else  //interaction prompt show / hide
        {
            prompt.Hide();
        }
    }

}
