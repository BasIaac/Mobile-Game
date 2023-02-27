using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class Client : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private TextMeshProUGUI feedbackText;
    
    [Header("Settings")]
    public ProductData expectedData;

    private Product feedbackProduct;
    private bool hasGivenBaseProduct;

    // TODO - Handle with Client Manager/GameManager
    private void Start()
    {
        SelectProduct(false);
    }

    // TODO - Should be configurable
    private void SelectProduct(bool random)
    {
        hasGivenBaseProduct = false;
        if (random)
        {
            expectedData = ProductData.Random;
        }
        
        feedbackProduct ??= new Product(expectedData);
        feedbackProduct.data = expectedData;

        feedbackText.text = $"{feedbackProduct}";
    }

    public void TakeProduct()
    {
        
    }
    
    public Product GiveBaseProduct()
    {
        if (hasGivenBaseProduct) return null;
        
        var baseData = new ProductData()
        {
            Color = ProductColor.White,
            Shape = ProductShape.Good,
        };

        hasGivenBaseProduct = true;
        return new Product(baseData);
    }

    public Product ReceiveProduct(Product product)
    {
        if (product.data != expectedData) return product;
        
        
        StopClient();
        return null;

    }

    public void StopClient()
    {
        feedbackText.text = $"Yay";

        StartCoroutine(NewProductDelay());
        
        IEnumerator NewProductDelay()
        {
            yield return new WaitForSeconds(5f);
            SelectProduct(true);
        }
    }
}
