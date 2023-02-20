using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    [SerializeField] private List<MachineSlot> slots = new List<MachineSlot>();
    [SerializeField] private List<Machine> machines = new List<Machine>();

    public void InitMachines()
    {
        MachineSlot.InitSlots();
        
        machines.Clear();
        foreach (var slot in slots)
        {
            machines.Add(slot.machine);
        }
    }
}
