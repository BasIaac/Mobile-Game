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
            return;
        }

        outProduct = client.ReceiveProduct(inProduct);
    }
}
