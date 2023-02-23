using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class LevelCreator : MonoBehaviour
{
   [Header("Key Gen")]
   [SerializeField] private string outputPourLesGP;
   
    [SerializeField] private LevelInitializer levelInitializer;
    [SerializeField] private Grid grid;
    public Vector2Int levelSize;
    private List<Tile> childList = new();
    private List<Tile> sortedList = new();

   [ContextMenu("Setup Level Creation")]
   public void Setup()
   {
      grid = GetComponent<Grid>();
   }
   
   [ContextMenu("InitGridSize")]
   public void InitGrid()
   {
      outputPourLesGP = String.Empty;
      ClearGrid();

      for (int i = 0; i < levelSize.x; i++)
      {
         for (int j = 0; j < levelSize.y; j++)
         {
            Instantiate(levelInitializer.blockData[0].caseObject, new Vector3(i +.5f, -.5f, j+.5f), Quaternion.identity, transform);
         }
      }
   }

   private void ClearGrid()
   {
      var childCount = transform.childCount;
      for (int i = 0; i < childCount; i++)
      {
         DestroyImmediate(transform.GetChild(0).gameObject);
      }
   }

   public void GenerateKey()
   {
      GetAllTiles();
      SortTile();

      for (int i = 0; i < childList.Count; i++)
      {
         outputPourLesGP += GetChar(sortedList[i].typeCases);
      }

      levelInitializer.levelSize = levelSize;
      levelInitializer.levelStringKey = outputPourLesGP;
      levelInitializer.Bake();
   }

   private void GetAllTiles()
   {
      childList.Clear();
      for (int i = 0; i < transform.childCount; i++)
      {
         Tile tempTile = transform.GetChild(i).GetComponent<Tile>();
         childList.Add(tempTile);

         var position = tempTile.transform.position;
         tempTile.posOnTileSet = new Vector3Int((int)((int)position.x + 0.5f), 0, (int)((int)position.z + 0.5f));
      }
   }

   private void SortTile()
   {
      sortedList.Clear();
      sortedList = childList.ToList();
      sortedList.Sort();
   }

   private char GetChar(TypeCases typeCases)
   {
      return typeCases switch
      {
         TypeCases.Empty => 'e',
         TypeCases.Obstacle => 'o',
         TypeCases.Floor => 'f',
         TypeCases.Machine => 'm',
         TypeCases.AnchorMachinePoint => 'a',
         _ => '\0'
      };
   }

   [ContextMenu("BuildLevel")]
   public void BuildLevel()
   {
      GenerateKey();
      levelInitializer.levelSize = levelSize;
      levelInitializer.levelStringKey = outputPourLesGP;
      levelInitializer.Bake();
   }
}
