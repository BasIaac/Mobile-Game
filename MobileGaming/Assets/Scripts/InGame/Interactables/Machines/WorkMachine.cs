using UnityEngine;

public class WorkMachine : Machine
{
    [Header("Work Settings")]
    public bool changeColor;
    public ProductColor targetColor;
    public bool changeShape;
    public ProductShape targetShape;
    
    protected override void Work()
    {
        if (changeColor) currentProduct.data.Color = targetColor;
        if (changeShape) currentProduct.data.Shape = targetShape;
    }
}
