using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Client : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private Image feedbackImage;
    [SerializeField] private TextMeshProUGUI feedbackText;

    [Header("Satisfaction Settings")]
    public float baseSatisfaction = 45f;
    private float currentSatisfaction = 0;
    [SerializeField] private float decayPerSecond = 1f;
    [SerializeField] private float increaseOnProduct;
    
    [HideInInspector] public ClientData data;
    private ProductData expectedData => data.productDatas[currentDataIndex];
    
    private int currentDataIndex = 0;
    private Product feedbackProduct;

    private Coroutine satisfactionRoutine;
    private WaitForSeconds satisfactionWait = new (0.1f);

    private void Start()
    {
        UpdateFeedbackImage();
        feedbackText.text = string.Empty;
    }

    public Product ReceiveProduct(Product product)
    {
        if (product.data != expectedData) return product;
        
        NextProduct();
        return null;

    }

    private void NextProduct()
    {
        currentDataIndex++;
        currentSatisfaction = baseSatisfaction;
        
        feedbackText.text = "Yay";
        
        StartCoroutine(NewProductDelayRoutine());
        
        IEnumerator NewProductDelayRoutine()
        {
            yield return new WaitForSeconds(0.5f); // TODO - prob mettre l'expected data a null pendant cette periode

            satisfactionRoutine = StartCoroutine(SatisfactionRoutine());
            
            if (currentDataIndex >= data.productDatas.Length)
            {
                InvokeEndEvents();
                StopClient();
                yield break;
            }
            feedbackText.text = $"{data.name} : \n{expectedData.Color} and {expectedData.Shape}";  
        }

        IEnumerator SatisfactionRoutine()
        {
            while (currentSatisfaction > 0)
            {
                yield return satisfactionWait;
                currentSatisfaction -= 0.1f * decayPerSecond;
                UpdateFeedbackImage();
            }
            
            StopClient();
        }
    }

    public void StopClient()
    {
        OnClientAvailable?.Invoke();
        
        StopCoroutine(satisfactionRoutine);
        satisfactionRoutine = null;
        currentSatisfaction = 0;
        UpdateFeedbackImage();
    }

    private void InvokeEndEvents()
    {
        OnEnd?.Invoke(data.points);
    }

    public event Action<int> OnEnd; 

    public void SetData(ClientData newData)
    {
        data = newData;
        currentDataIndex = -1;
        
        NextProduct();
    }

    public event Action OnClientAvailable;

    public void UpdateFeedbackImage()
    {
        feedbackImage.fillAmount = currentSatisfaction / baseSatisfaction;
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(Client)),CanEditMultipleObjects]
    public class ClientEditor : Editor
    {
        private int productDataCount = 0;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var client = (Client)target;
            
            EditorGUILayout.LabelField("Product Settings",EditorStyles.boldLabel);
            
            EditorGUILayout.BeginHorizontal();
            client.data.name = EditorGUILayout.TextField("Client Name",client.data.name);
            client.data.points = EditorGUILayout.IntField("Points",client.data.points);
            EditorGUILayout.EndHorizontal();
            
            productDataCount = EditorGUILayout.IntField("Product Count", productDataCount);

            var currentLenght = client.data.productDatas.Length;
            if (currentLenght != productDataCount)
            {
                var data = new ProductData[productDataCount];
                
                for (int i = 0; i < (currentLenght < productDataCount ? currentLenght : productDataCount); i++)
                {
                    data[i] = client.data.productDatas[i];
                }

                client.data.productDatas = data;
            }

            for (var index = 0; index < productDataCount; index++)
            {
                var productData = client.data.productDatas[index];
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel($"Product {index}");
                productData.Color = (ProductColor)EditorGUILayout.EnumPopup(productData.Color);
                productData.Shape = (ProductShape)EditorGUILayout.EnumPopup(productData.Shape);
                EditorGUILayout.EndHorizontal();
                client.data.productDatas[index] = productData;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(15);
            if (GUILayout.Button("+",GUILayout.Width(25)))
            {
                productDataCount++;
            }
            if (GUILayout.Button("-",GUILayout.Width(25)))
            {
                if(productDataCount>= 1) productDataCount--;
            }
            EditorGUILayout.EndHorizontal();
            
        }
    }
#endif
}

[Serializable]
public struct ClientData
{
    public string name;
    public int points;
    public ProductData[] productDatas;
}
