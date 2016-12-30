using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomPropertyDrawer(typeof(FloatClamp))]
public class FloatClampDrawer : PropertyDrawer
{
    public FloatClamp.ClampType clamptype;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        GUIStyle M_STYLE = new GUIStyle();
        M_STYLE.fontSize = 9;
        M_STYLE.wordWrap = true; 

        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);
        
        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label, M_STYLE);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

       // clamptype = (FloatClamp.ClampType) EditorGUI.EnumPopup(new Rect(position.x + 60, position.y, 30, position.height), "CTYPE", clamptype);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        Rect _valueRect = new Rect(position.x, position.y, 90, position.height);

        EditorGUI.PropertyField(_valueRect, property.FindPropertyRelative("_ctype"), GUIContent.none);

       
       if (property.FindPropertyRelative("_ctype").enumValueIndex == (int) FloatClamp.ClampType.value)
        {
            EditorGUI.PropertyField(_valueRect.Move(100,0), property.FindPropertyRelative("_value"), GUIContent.none);
        }

       

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
