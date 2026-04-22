using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;  //taking the position of the interaction point
    [SerializeField] private float interactionPointRadius = 0.5f;  //how large the range is for the player to interact
    [SerializeField] private LayerMask interactableMask;  //what shows up when in range

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);  //counts how many interaction points are in radius

        if (numFound > 0)
        { //if there is an interactable object in front of the player and you have pressed the E key, then we can interact since we are the interactor interacting (lol)
            var interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                interactable.Interact(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
