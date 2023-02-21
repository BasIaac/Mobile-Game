using System;
using System.Linq;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    public string levelStringKey;
    private char c;
    private int blockIndex;

    [Header("Params")] 
    
    public Vector2Int levelSize;
    [Space(10)] [SerializeField] private BlockData[] blockData;
    
    public void Bake()
    {
        ClearGrid();
        
        for (int x = 0; x < levelSize.x; x++)
        {
            for (var y = 0; y < levelSize.y; y++)
            {
                c = levelStringKey[blockIndex]; 
                Debug.Log(c);
                GameObject GO = Instantiate(SetBlockByChar(c), new Vector3(x, 0, y), Quaternion.identity, transform);
                blockIndex++;
            }
        }
    }

    private GameObject SetBlockByChar(char c)
    {
        return (from data in blockData where data.c == c select data.caseObject).FirstOrDefault();
    }

    public void ClearGrid()
    {
        blockIndex = 0;
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}

public enum TypeCases
{
    Empty, Obstacle, Floor, Machine, AnchorMachinePoint
}

[Serializable]
public struct BlockData
{
    public char c;
    public TypeCases type;
    public GameObject caseObject;
}
