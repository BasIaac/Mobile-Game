using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public bool Interactable { get; private set; }
    
    [Header("Settings")]
    [SerializeField] private float timeToProduce = 5f;
    [SerializeField] private float timeMultiplier = 1f;
    
    private Product currentProduct;

    public void LoadProduct(Product product)
    {
        
    }

    public void UnloadProduct(out Product product)
    {
        product = currentProduct;
    }
}
