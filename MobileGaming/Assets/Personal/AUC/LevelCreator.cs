using System;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
   public LevelInitializer levelInitializer;
   
   public int levelSize;
   public string outputPourLesGP;
   [Header("Level In Inspector")]
   public level data;

   [ContextMenu("InitGridSize")]
   public void InitSize()
   {
      data.size = levelSize;
      data.rows = new level.rowData[levelSize];
   }
   
   public void GenerateKey()
   {
      outputPourLesGP = String.Empty;
      for (int x = 0; x < data.rows.Length; x++)
      {
         for (int y = 0; y < data.rows[x].row.Length;y++)
         {
            outputPourLesGP += GetChar(data.rows[x].row[y]);
         }
      }
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
         _ => '\n'
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

[Serializable]
public class level
{
   [Serializable] 
   public struct rowData
   {
      public TypeCases[] row;
   }

   public int size = 5;
   public rowData[] rows = new rowData[5];
}



