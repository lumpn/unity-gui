//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEditor;
using UnityEngine;

namespace Lumpn.UGUI
{
    [CustomEditor(typeof(LayoutGroup))]
    public sealed class LayoutGroupEditor : Editor<LayoutGroup>
    {
        public override void OnInspectorGUI(LayoutGroup target)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Apply Layout");

            if (GUILayout.Button("Horizontal"))
            {
                target.ApplyHorizontal();
                EditorUtility.SetDirty(target);
            }

            if (GUILayout.Button("Vertical"))
            {
                target.ApplyVertical();
                EditorUtility.SetDirty(target);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Fit Size");

            if (GUILayout.Button("Horizontal"))
            {
                target.FitHorizontal();
                EditorUtility.SetDirty(target);
            }

            if (GUILayout.Button("Vertical"))
            {
                target.FitVertical();
                EditorUtility.SetDirty(target);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
