using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SubMenuScaling))]
public class SubMenuScalingCustomInspector : Editor
{
    private SerializedProperty width;
    private SerializedProperty height;

    /// <summary>
    /// Set connection to properties which need to be updated
    /// </summary>
    private void OnEnable()
    {
        width = serializedObject.FindProperty("width");
        height = serializedObject.FindProperty("height");
    }
    /// <summary>
    /// Create CustomInspector design for SubMenuScaling scripts
    /// </summary>
    public override void OnInspectorGUI()
    {
        SubMenuScaling myTarget = (SubMenuScaling)target;

        serializedObject.UpdateIfRequiredOrScript();

        EditorGUILayout.PropertyField(width, new GUIContent("Width"));
        EditorGUILayout.PropertyField(height, new GUIContent("Height"));

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("NameTag Position", "Horizontal: " + myTarget.horizontalAlignmentText + " - vertical: " + myTarget.verticalAlignmentText);
        EditorGUI.indentLevel++;
        EditorGUILayout.LabelField("Horizontal", "");
        if (GUILayout.Button("Left"))
        {
            myTarget.setHorizontalLeft();
        }
        if (GUILayout.Button("Center"))
        {
            myTarget.setHorizontalCenter();
        }
        if (GUILayout.Button("Right"))
        {
            myTarget.setHorizontalRight();
        }
        EditorGUILayout.LabelField("Vertical", "");
        if (GUILayout.Button("Top"))
        {
            myTarget.setVerticalTop();
        }
        if (GUILayout.Button("Center"))
        {
            myTarget.setVerticalCenter();
        }
        if (GUILayout.Button("Bottom"))
        {
            myTarget.setVerticalBottom();
        }
        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }
}
