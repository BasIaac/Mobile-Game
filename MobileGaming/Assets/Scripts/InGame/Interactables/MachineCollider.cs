using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MachineCollider : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private MachineSlot machineSlot;

    private void OnTriggerEnter(Collider other)
    {
        machineSlot.EnterRange();
    }

    private void OnTriggerExit(Collider other)
    {
        machineSlot.ExitRange();
    }
}
