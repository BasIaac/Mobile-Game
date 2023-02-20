using System;
using UnityEngine;

public class MachineSlot : MonoBehaviour, IInteractable
{
    [field:SerializeField] public Machine machine { get; private set; }
    
    public void SetMachine(Machine newMachine)
    {
        machine = newMachine;
    }

    public void Interact(Product inProduct,out Product outProduct)
    {
        outProduct = null;
        if (inProduct is null)
        {
            machine.UnloadProduct(out outProduct);
            return;
        }
        
        machine.LoadProduct(inProduct);
    }

    public void EnterRange()
    {
        OnRangeEnter?.Invoke(this);
    }

    public void ExitRange()
    {
        OnRangeExit?.Invoke(this);
    }

    public static void InitSlots()
    {
        OnRangeEnter = null;
        OnRangeExit = null;
    }

    public static event Action<IInteractable> OnRangeEnter;
    public static event Action<IInteractable> OnRangeExit;
}
