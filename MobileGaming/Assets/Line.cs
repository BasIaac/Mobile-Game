using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [Tooltip("Layer to avoid collision")]
    [SerializeField] private LayerMask linkLayer;
    [Tooltip("Two materials to show if the line collide or not with others lines. \n 0 = Not collide / 1 = Collide")]
    [SerializeField] private Material[] linkMaterials;
    [Tooltip("Time to wait between 2 reset of baking Mesh Collider")] 
    [SerializeField] private float resetCollisionDetectionTime = 0.25f;

    private MeshCollider meshCollider;

    private LineRenderer myLR;
    
    // Start is called before the first frame update
    void Start()
    {
        linkLayer = LayerMask.NameToLayer("Link");
        myLR = GetComponent<LineRenderer>();
        myLR.material = linkMaterials[0];
        StartCoroutine(Reset(resetCollisionDetectionTime));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        myLR.material = linkMaterials[1];
        Debug.LogWarning("Collision avec un autre lien magique");
    }

    private void OnTriggerExit(Collider other)
    {
        myLR.material = linkMaterials[0];
        Debug.LogWarning("Sortie de collision avec un autre lien magique");
    }

    IEnumerator Reset(float _resetTime)
    {
        myLR.Simplify(0.05f);
        GenerateMeshCollider();
        yield return new WaitForSeconds(_resetTime);
        StartCoroutine(Reset(_resetTime));
    }
     
    private void GenerateMeshCollider()
    {
        if (meshCollider == null)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
            meshCollider.convex = true;
            meshCollider.isTrigger = true;
        }
        
        Mesh mesh = new Mesh();
        myLR.BakeMesh(mesh, true);
        meshCollider.sharedMesh = mesh;
    }
}
