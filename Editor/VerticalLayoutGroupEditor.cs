using UnityEditor;
using UnityEngine;

namespace Lumpn.UGUI
{
    [CustomEditor(typeof(VerticalLayoutGroup))]
    public sealed class VerticalLayoutGroupEditor : Editor<VerticalLayoutGroup>
    {
        public override void OnInspectorGUI(VerticalLayoutGroup target)
        {
            if (GUILayout.Button("Apply"))
            {
                target.Apply();
            }
        }
    }
}
