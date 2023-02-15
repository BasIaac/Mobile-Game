using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour, IInteractible
{
    public bool Interactable { get; private set; }

    [Header("Dependencies")]
    public static SorcererController sorcerer; // TODO - Make a machine Manager that handles this connection
    
    [Header("Settings")]
    [SerializeField] private float timeToProduce = 5f;
    [SerializeField] private float timeMultiplier = 1f;
    
    private Product currentProduct;

    public void Interact(Product product)
    {
        Debug.Log($"Interacting ({product})");
    }

    public void SetInteractible()
    {
        sorcerer.SetInteractible(this);
    }

    public void UnSetInteractible()
    {
        sorcerer.SetInteractible(null);
    }
}
