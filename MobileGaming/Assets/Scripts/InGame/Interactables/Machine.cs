using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Machine : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private TextMeshProUGUI feedbackText;
    
    [Header("Settings")]
    [SerializeField] private float baseTimeToProduce = 5f;
    [SerializeField] private float timeMultiplier = 1f;

    private Coroutine workRoutine;

    protected double timer { get; private set; }
    protected double waitDuration { get; private set; }
    protected Product currentProduct;

    public void LoadProduct(Product product)
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
            
            UpdateFeedbackText($"{timer/waitDuration:P}%");
        }
        
        Work();
        
        EndWork();
    }
    
    protected abstract void Work();

    private void EndWork()
    {
        UpdateFeedbackText("Work Done");
        
        workRoutine = null;
    }

    public void UnloadProduct(out Product product)
    {
        product = null;
        
        if(workRoutine != null) return;
        
        product = currentProduct;

        currentProduct = null;
        
        UpdateFeedbackText("No Product");
    }

    public void UpdateFeedbackText(string text)
    {
        feedbackText.text = text;
    }
}
