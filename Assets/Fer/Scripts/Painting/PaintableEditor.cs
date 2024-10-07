/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Paintable))]
public class PaintableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Paintable paintable = (Paintable)target;
        if (paintable.getExtend() != null)
        {
            EditorGUILayout.LabelField("Texture Preview", EditorStyles.boldLabel);
            Rect rect = GUILayoutUtility.GetRect(100, 100);
            EditorGUI.DrawPreviewTexture(rect, paintable.getExtend());

        }
        else
        {
            EditorGUILayout.LabelField("Texture no disponible");
        }
    }
}
*/