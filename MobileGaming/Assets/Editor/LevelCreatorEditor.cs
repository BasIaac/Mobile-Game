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
         if (GUILayout.Button("SetupGrid")) { levelCreator.InitGrid(); }
         if (GUILayout.Button("Generate Key")) { levelCreator.GenerateKey(); }
         GUILayout.EndHorizontal();
         GUILayout.BeginVertical();
         if (GUILayout.Button("Generate Key")) { levelCreator.GenerateKey(); }
         GUILayout.EndVertical();
         
     }
 }