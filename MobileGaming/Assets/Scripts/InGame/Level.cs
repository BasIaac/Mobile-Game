using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Setup with tool by GD")]
    public List<ClientTiming> clientsDatas = new ();

    [Header("Setup with tool automatically")]
    public List<Client> clients = new ();


    private Queue<ClientData> queuedData = new ();

    private void Start()
    {
        queuedData.Clear();
    }

    private void Update()
    {
        
    }
}
