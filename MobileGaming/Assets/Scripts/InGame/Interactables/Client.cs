using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour, IInteractable
{
    public ProductData expectedProduct;
    
    private bool hasGivenBaseProduct;
    
    public void Interact(Product inProduct, out Product outProduct)
    {
        outProduct = inProduct;
        if (inProduct is null)
        {
            outProduct = GiveBaseProduct();
            return;
        }

        if (inProduct.data != expectedProduct) return;
        
        outProduct = null;
        StopClient();
    }

    private Product GiveBaseProduct()
    {
        var baseData = new ProductData()
        {
            Color = ProductColor.White,
            Shape = ProductShape.Good,
            Size = ProductSize.Normal
        };
        
        return hasGivenBaseProduct ? null : new Product(baseData);
    }

    private void StopClient()
    {
        
    }
}
