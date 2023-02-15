using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MachineCollider : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Machine machine;

    private void OnTriggerEnter(Collider other)
    {
        machine.SetInteractible();
    }

    private void OnTriggerExit(Collider other)
    {
        machine.UnSetInteractible();
    }
}
