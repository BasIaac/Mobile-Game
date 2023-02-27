using System;
using Unity.VisualScripting;
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
        if (machine is null)
        {
            outProduct = inProduct;
            return;
        }
        
        outProduct = null;
        if (inProduct is null)
        {
            machine.UnloadProduct(out outProduct);
            return;
        }
        
        machine.LoadProduct(inProduct);
    }

}
