using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLink : MonoBehaviour
{
    #region Variables
    
    private DrawMagicLine lineInCollision;
    public List<Machine> machinesInLinks;
    public Material myMaterial;
    
    // Magic Transportation
    public Product product = default;
    [Range(0,100)] public int itemProgression;
    public bool canRool;
    
    public float timeToCompleteTransportation = 10f;
    public float currentTimer;
    
    #endregion

    private void Start()
    {
        myMaterial = GetComponent<LineRenderer>().material;
    }
    
    private void Update()
    {
        if (machinesInLinks[1].IsProduct()) return;

        if (product == null) TakeProduct();
        
        currentTimer += Time.deltaTime;
        if (currentTimer > timeToCompleteTransportation)
        {
            DeliverProduct(product);
            itemProgression = 0;
        }
        
        Feedback();
    }

    private void TakeProduct()
    {
        if (!machinesInLinks[0].IsProduct() && product is null)
        {
            machinesInLinks[0].UnloadProduct(out product);
        }
    }
    
    private void DeliverProduct(Product _product)
    {
        machinesInLinks[1].LoadProduct(_product);
        product = null;
    }

    private void Feedback()
    {
        itemProgression =  (int)((currentTimer / timeToCompleteTransportation) * 100);
        myMaterial.SetFloat("_FilingValue", currentTimer / timeToCompleteTransportation);
    }


    #region Event Methods

    private void OnTriggerEnter(Collider other)
    {
        lineInCollision = other.GetComponent<DrawMagicLine>();
        if (lineInCollision is null) return;
        lineInCollision.myLR.material = lineInCollision.linkMaterials[1];
        Debug.LogWarning("Collision avec un autre lien magique");
        lineInCollision.isLinkable = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (lineInCollision is null) return;
        lineInCollision.myLR.material = lineInCollision.linkMaterials[0];
        Debug.LogWarning("Sortie de collision avec un autre lien magique");
        lineInCollision.isLinkable = true;
        lineInCollision = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }

    #endregion
}
