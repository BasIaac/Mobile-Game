using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Level : MonoBehaviour
{
    [Header("Setup with tool by GD")]
    public List<ClientTiming> clientsDatas = new ();

    [Header("Setup with tool automatically")]
    public List<Client> clients = new ();
    
    private Queue<ClientTiming> queuedTimings = new ();
    private Queue<Client> availableClients = new();

    private ClientTiming nextTiming;
    private Client availableClient;
    private double startTime;
    private double maxTime = 0;

    private void Start()
    {
        queuedTimings.Clear();
        availableClients.Clear();
        
        SetupQueue();
        
        SubscribeClients();
        
        startTime = Time.time;
    }

    private void SetupQueue()
    {
        clientsDatas.Sort();
        maxTime = 0;
        foreach (var clientData in clientsDatas)
        {
            queuedTimings.Enqueue(clientData);
            if (maxTime < clientData.time) maxTime = clientData.time;
        }
    }

    private void SubscribeClients()
    {
        foreach (var client in clients)
        {
            client.OnClientAvailable += UpdateAvailableClient;
            UpdateAvailableClient();
            
            void UpdateAvailableClient()
            {
                availableClients.Enqueue(client);
            }
        }
    }

    private void Update()
    {
        UpdateQueue();
    }

    private void UpdateQueue()
    {
        if(Time.time - startTime < nextTiming.time) return;
        
        if(!queuedTimings.TryPeek(out nextTiming) || !availableClients.TryPeek(out availableClient)) return;
        
        availableClients.Dequeue().SetData(queuedTimings.Dequeue().data);

        nextTiming.time += maxTime;
        queuedTimings.Enqueue(nextTiming);
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(Level)),CanEditMultipleObjects]
    public class LevelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var level = (Level)target;
            
            EditorGUILayout.LabelField("Client Settings",EditorStyles.boldLabel);
            
            EditorGUILayout.IntField("Product Count", level.clientsDatas.Count); // TODO - change array size on modification

            foreach (var timing in level.clientsDatas)
            {
                var client = timing.data;
                
                EditorGUILayout.LabelField("Product Settings",EditorStyles.boldLabel);
            
                client.name = EditorGUILayout.TextField("Client Name",client.name);
                
                for (var index = 0; index < client.productDatas.Length; index++)
                {
                    var productData = client.productDatas[index];
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel($"Product {index}");
                    productData.Color = (ProductColor)EditorGUILayout.EnumPopup(productData.Color);
                    productData.Shape = (ProductShape)EditorGUILayout.EnumPopup(productData.Shape);
                    EditorGUILayout.EndHorizontal();
                    client.productDatas[index] = productData;
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(15);
                if (GUILayout.Button("+",GUILayout.Width(25)))
                {
                    client.productDatas = new ProductData[client.productDatas.Length + 1];
                }
                if (GUILayout.Button("-",GUILayout.Width(25)))
                {
                    if(client.productDatas.Length > 0) client.productDatas = new ProductData[client.productDatas.Length - 1];
                }
                EditorGUILayout.EndHorizontal();
                
                
            }
            
            
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(15);
            if (GUILayout.Button("+",GUILayout.Width(25)))
            {
                level.clientsDatas.Add(new ClientTiming()
                {
                    data = new ClientData()
                    {
                        productDatas = Array.Empty<ProductData>()
                    }
                });
            }
            if (GUILayout.Button("-",GUILayout.Width(25)))
            {
                if (level.clientsDatas.Count > 0)
                {
                    level.clientsDatas.RemoveAt(level.clientsDatas.Count-1);
                }
            }
            EditorGUILayout.EndHorizontal();
            
        }
    }
#endif
}

[Serializable]
public struct ClientTiming : IComparable<ClientTiming>
{
    public double time;
    public ClientData data;

    public int CompareTo(ClientTiming other)
    {
        return time.CompareTo(other.time);
    }

    public override string ToString()
    {
        return $"Timing at {time}";
    }
}
