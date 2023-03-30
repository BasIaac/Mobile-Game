#nullable enable
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MachineLink : MonoBehaviour
{
    #region Variables
    
    public TextMeshProUGUI debugPercentageText;
    private DrawMagicLine lineInCollision;
    public List<Machine> machinesInLinks;
    public Material myMaterial;
    
    // Magic Transportation
    public Product productInTreatment;
    [Range(0,100)] public int itemProgression = 0;
    
    public float timeToCompleteTransportation = 10f;
    public float currentTimer = 0f;
    
    #endregion

    private void Start()    
    {
        myMaterial = GetComponent<LineRenderer>().material;
    }
    
    private void Update()
    {
        if (machinesInLinks[1].IsProduct()) return;

        if (productInTreatment == null && machinesInLinks[0].IsProduct())
        {
            if (machinesInLinks[0].GetComponent<GenerationMachine>() != null)
                TakeProductFromMachine(machinesInLinks[0].GetComponent<GenerationMachine>().newProduct);
            
            Debug.Log(productInTreatment.data.Color);
            Debug.Log(productInTreatment.data.Shape);
        }
        
        currentTimer += Time.deltaTime;
        if (currentTimer > timeToCompleteTransportation)
        {
            DeliverProductIntoMachine();
            currentTimer = 0;
        }
        
        Feedback();
    }

    public void TakeProductFromMachine(Product _product)
    {
        productInTreatment = _product;
    }
    
    private void DeliverProductIntoMachine()
    {
        machinesInLinks[1].ReceiveProductFromLink(productInTreatment);
        productInTreatment = null;
    }

    private void Feedback()
    {
        itemProgression =  (int)((currentTimer / timeToCompleteTransportation) * 100);
        myMaterial.SetFloat("_FilingValue", 1 - currentTimer / timeToCompleteTransportation);
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
