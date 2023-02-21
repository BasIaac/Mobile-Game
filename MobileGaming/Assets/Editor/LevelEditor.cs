 using UnityEngine;
 using UnityEditor;
 
 
 [CustomEditor(typeof(LevelCreator))]
 public class LevelEditor : Editor {
     public override void OnInspectorGUI() 
     {
         base.OnInspectorGUI();
         EditorGUILayout.Space(20);
         LevelCreator levelCreator = (LevelCreator)target;
         GUILayout.BeginVertical();
         if (GUILayout.Button("Setup Size")) { levelCreator.InitSize(); }
         if (GUILayout.Button("Generate Key")) { levelCreator.GenerateKey(); }
         GUILayout.EndVertical();
     }
 }