using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractableCollider : MonoBehaviour
{
    [Header("Settings")]
    [SerializeReference] private Interactable interactable;

    private void OnTriggerEnter(Collider other)
    {
        interactable.EnterRange();
    }

    private void OnTriggerExit(Collider other)
    {
        interactable.ExitRange();
    }
}
