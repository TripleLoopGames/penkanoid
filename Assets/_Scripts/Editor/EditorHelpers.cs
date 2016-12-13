using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditorUtils))]
public class EditorHelpers : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorUtils myScript = (EditorUtils)this.target;
        if (GUILayout.Button("Add component"))
        {
            myScript.AddComponentInChildren();
        }
    }
}
