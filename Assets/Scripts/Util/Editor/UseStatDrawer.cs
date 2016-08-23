using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(UseStat))]
public class UseStatDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

 

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.LabelField(new Rect(position.x, position.y, 60, position.height), "USE STAT?");
        EditorGUI.PropertyField(new Rect(position.x+ 60, position.y, 30, position.height), property.FindPropertyRelative("Use"), GUIContent.none);

        Rect _valueRect = new Rect(position.x + 90, position.y, 60, position.height);
         if (property.FindPropertyRelative("Use").boolValue)
            { 
            EditorGUI.PropertyField(_valueRect, property.FindPropertyRelative("StatType"), GUIContent.none);
         } else
         {
            EditorGUI.PropertyField(_valueRect, property.FindPropertyRelative("DefaultValue"), GUIContent.none);
            }


        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
