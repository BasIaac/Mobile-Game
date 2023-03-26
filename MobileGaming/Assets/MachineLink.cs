using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLink : MonoBehaviour
{
    #region Variables
    private DrawMagicLine lineInCollision;
    public List<Machine> machinesInLinks;
    [Range(0,100)] public int itemProgression;
    
    #endregion

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
