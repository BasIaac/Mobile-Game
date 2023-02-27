using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
