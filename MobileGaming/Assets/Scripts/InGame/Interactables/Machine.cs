using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Machine : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private Image feedbackImage;
    [SerializeField] private GameObject feedbackObject; //TODO Make only one object (one per product), not one per machine and teleport it
    
    [Header("Production Settings")]
    [SerializeField] private float baseTimeToProduce = 5f;
    [SerializeField] private float timeMultiplier = 1f;
    
    private Coroutine workRoutine;

    protected double timer { get; private set; }
    protected double waitDuration { get; private set; }
    protected Product currentProduct;

    
    public virtual void LoadProduct(Product inProduct,out Product outProduct)
    {
        
    }
    

    public virtual void LoadProduct(Product product)
    {
        if(workRoutine != null) return;
        
        currentProduct = product;
        waitDuration = baseTimeToProduce * 1f / timeMultiplier;

        workRoutine = StartCoroutine(WorkProduct());
    }

    private IEnumerator WorkProduct()
    {
        timer = 0;
        while (timer < waitDuration)
        {
            yield return null;
            timer += Time.deltaTime;
            
            UpdateFeedbackText(1 - timer/waitDuration);
            
            UpdateFeedbackObject();
        }
        
        Work();
        
        EndWork();
    }
    
    protected abstract void Work();

    private void EndWork()
    {
        UpdateFeedbackText(0);
        
        UpdateFeedbackObject();
        
        workRoutine = null;
    }

    public virtual void UnloadProduct(Product inProduct,out Product outProduct)
    {
        outProduct = inProduct;
        
        if(workRoutine != null) return;
        
        outProduct = currentProduct;

        currentProduct = null;
        
        UpdateFeedbackText(0);
        
        UpdateFeedbackObject();
    }

    public void UpdateFeedbackText(double amount)
    {
        if(feedbackImage is null) return;
        feedbackImage.fillAmount = (float)amount;
    }

    public void UpdateFeedbackObject()
    {
        if(feedbackObject == null) return;
        feedbackObject.SetActive(currentProduct != null);
    }
}
