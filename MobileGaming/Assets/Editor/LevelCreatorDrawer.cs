using UnityEditor;
using UnityEngine;

public class LevelCreatorDrawer : PropertyDrawer
{
   public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
   {
      EditorGUI.PrefixLabel(position, label);
      Rect newPosition = position;
      newPosition.y += 20f;
      SerializedProperty data = property.FindPropertyRelative("rows");
      int size = property.FindPropertyRelative("size").intValue;
      
      for (int i = 0; i < size; i++)
      {
         SerializedProperty row = data.GetArrayElementAtIndex(i).FindPropertyRelative("row");
         newPosition.height = 20f;
         if (row.arraySize != size) row.arraySize = size;
         newPosition.width = position.width / size;

         for (int j = 0; j < size; j++)
         {
            EditorGUI.PropertyField(newPosition, row.GetArrayElementAtIndex(j), GUIContent.none);
            newPosition.x += newPosition.width;
         }

         newPosition.x = position.x;
         newPosition.y += 20f;
      }
   }

   public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
   {
      return 18f * 22f;
   }
}
