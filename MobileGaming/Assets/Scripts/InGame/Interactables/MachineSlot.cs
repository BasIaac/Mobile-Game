using System;
using UnityEngine;

public class MachineSlot : Interactable
{
    [field:SerializeField] public Machine machine { get; private set; }
    
    public void SetMachine(Machine newMachine)
    {
        machine = newMachine;
    }

    public override void Interact(Product inProduct,out Product outProduct)
    {
        outProduct = null;
        if (inProduct is null)
        {
            machine.UnloadProduct(out outProduct);
            return;
        }
        
        machine.LoadProduct(inProduct);
    }

}
