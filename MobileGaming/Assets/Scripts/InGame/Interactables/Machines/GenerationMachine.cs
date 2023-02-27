using UnityEngine;

public class GenerationMachine : Machine
{
    [Header("Work Settings")]
    public Product newProduct;
    
    protected override void Work()
    {
        
    }

    public override void UnloadProduct(out Product product)
    {
        product = newProduct;
    }
}
