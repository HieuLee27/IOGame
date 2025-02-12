using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Map))]
public class EditorMap : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Map map = (Map)target;
        if(GUILayout.Button("Generate Map"))
        {
            map.PainMap();
        }
    }
}
