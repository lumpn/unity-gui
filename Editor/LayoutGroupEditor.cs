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
        [SerializeField] private bool reverse;

        public override void OnInspectorGUI(LayoutGroup target)
        {
            reverse = EditorGUILayout.Toggle("Reverse", reverse);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Apply Layout");

            if (GUILayout.Button("Horizontal"))
            {
                target.ApplyHorizontal(reverse);
                EditorUtility.SetDirty(target);
            }

            if (GUILayout.Button("Vertical"))
            {
                target.ApplyVertical(reverse);
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
