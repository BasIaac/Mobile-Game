using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSlot : Interactable
{
    [field:SerializeField] public Client client { get; private set; }
    
    public override void Interact(Product inProduct, out Product outProduct)
    {
        Debug.Log("CLIENT");
        
        outProduct = inProduct;
        if (inProduct is null)
        {
            outProduct = client.GiveBaseProduct();
            return;
        }

        if (inProduct.data != client.expectedProduct) return;
        
        outProduct = null;
        client.StopClient();
    }
}
