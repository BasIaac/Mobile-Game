using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private Image feedbackImage;
    [SerializeField] private TextMeshProUGUI feedbackText;

    [Header("Satisfaction Settings")]
    public float baseSatisfaction = 45f;
    private float currentSatisfaction = 0;
    [SerializeField] private float increaseOnProduct;
    
    [Header("Product Settings")]
    public ClientData data;
    private ProductData expectedData => data.productDatas[currentDataIndex];
    
    public bool isAvailable { get; private set; }
    private int currentDataIndex = 0;
    private Product feedbackProduct;
    
    public Product ReceiveProduct(Product product)
    {
        if (product.data != expectedData) return product;
        
        NextProduct();
        return null;

    }

    private void NextProduct()
    {
        currentDataIndex++;
        
        feedbackText.text = "Yay";
        
        StartCoroutine(NewProductDelay());
        
        IEnumerator NewProductDelay()
        {
            yield return new WaitForSeconds(0.5f); // TODO - prob mettre l'expected data a null pendant cette periode

            if (currentDataIndex >= data.productDatas.Count)
            {
                StopClient();
                yield break;
            }
            feedbackText.text = $"{data.name} : \n {expectedData.Color} and {expectedData.Shape}";  
        }
    }

    public void StopClient()
    {
        isAvailable = true;
        OnClientAvailable?.Invoke();
    }

    public void SetData(ClientData newData)
    {
        isAvailable = false;

        data = newData;
        currentDataIndex = -1;
        currentSatisfaction = baseSatisfaction;
        
        NextProduct();
    }

    public event Action OnClientAvailable;
}

[Serializable]
public struct ClientData
{
    public string name;
    public List<ProductData> productDatas;
}
