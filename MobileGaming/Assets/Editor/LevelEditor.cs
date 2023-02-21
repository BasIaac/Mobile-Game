 using UnityEngine;
 using UnityEditor;
 
 
 [CustomEditor(typeof(LevelCreator))]
 public class LevelEditor : Editor {
 
     public bool showLevels = true;
 
     public override void OnInspectorGUI() 
     {
         base.OnInspectorGUI();
        
         LevelCreator levelInitializer = (LevelCreator)target;
         EditorGUILayout.Space(20);
         if (GUILayout.Button("Bake")) { levelInitializer.InitSize(); }
         EditorGUILayout.Space(5);
         if (GUILayout.Button("Clear")) { levelInitializer.BuildLevel(); }
         EditorGUILayout.Space(5);
         if (GUILayout.Button("Generate Key")) { levelInitializer.GenerateKey(); }
     }
 }