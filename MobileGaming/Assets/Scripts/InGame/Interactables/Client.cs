using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Client : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private TextMeshProUGUI feedbackText;
    
    [Header("Settings")]
    public ProductData expectedProduct;
    
    private bool hasGivenBaseProduct;

    public void TakeProduct()
    {
        
    }
    
    public Product GiveBaseProduct()
    {
        var baseData = new ProductData()
        {
            Color = ProductColor.White,
            Shape = ProductShape.Good,
            Size = ProductSize.Normal
        };
        
        return hasGivenBaseProduct ? null : new Product(baseData);
    }

    public void StopClient()
    {
        
    }
}
