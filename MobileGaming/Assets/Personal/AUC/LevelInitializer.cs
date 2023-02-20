using System;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    public static LevelInitializer instance;
    
    public string levelStringKey;
    private char c;
    private int blockIndex;

    [Header("Params")] public int levelSize;
    
    [Space(20)]
    [SerializeField] private BlockData[] blockData;

    [ContextMenu("Setup")]
    private void Awake()
    {
        instance = this;
        Bake();
    }
    
    public void Bake()
    {
        ClearGrid();
        
        for (int x = 0; x < levelSize; x++)
        {
            for (int y = 0; y < levelSize; y++)
            {
                var currentPos = new Vector2(x, y);
                if (levelStringKey[blockIndex] != '\n')
                {
                    c = levelStringKey[blockIndex]; 
                    Debug.Log(c);
                    GameObject GO = Instantiate(SetBlockByChar(c), new Vector3(currentPos.x, 0, currentPos.y), Quaternion.identity, transform);
                    blockIndex++;
                }
                else
                {
                    Debug.Log("Fini avec " + blockIndex  + " éléments posés");
                }
            }
        }
    }

    private GameObject SetBlockByChar(char c)
    {
        foreach (var data in blockData)
        {
            if(data.c != c) continue;
            return data.caseObject;
        }

        return null;
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
