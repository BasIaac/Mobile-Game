using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelInitializer))]
class LevelConstructorEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        LevelInitializer levelInitializer = (LevelInitializer)target;
        EditorGUILayout.Space(20);
        if (GUILayout.Button("Bake")) { levelInitializer.Bake(); }
        EditorGUILayout.Space(5);
        if (GUILayout.Button("Clear")) { levelInitializer.ClearGrid(); }
    }
}