using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSlot : Interactable
{
    [field:SerializeField] public Client client { get; private set; }
    
    public override void Interact(Product inProduct, out Product outProduct)
    {
        if (client is null)
        {
            outProduct = inProduct;
            return;
        }
        
        outProduct = inProduct;
        
        if (inProduct is null)
        {
            outProduct = client.GiveBaseProduct();
            return;
        }

        outProduct = client.ReceiveProduct(inProduct);
    }
}
