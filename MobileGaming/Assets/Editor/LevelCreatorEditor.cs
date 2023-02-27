 using UnityEngine;
 using UnityEditor;
 
 
 [CustomEditor(typeof(LevelCreator))]
 public class LevelCreatorEditor : Editor {
     public override void OnInspectorGUI() 
     {
         base.OnInspectorGUI();
         EditorGUILayout.Space(20);
         LevelCreator levelCreator = (LevelCreator)target;
         GUILayout.BeginHorizontal();
         if (GUILayout.Button("Setup Grid")) { levelCreator.InitGrid(); }
         if (GUILayout.Button("Clear Grid")) { levelCreator.ClearGrid(); }
         GUILayout.EndHorizontal();
         GUILayout.BeginVertical();
         if (GUILayout.Button("Generate Key")) { levelCreator.GenerateKey(); }
         GUILayout.EndVertical();
         
     }
 }