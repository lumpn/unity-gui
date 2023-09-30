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
                var rectTransform = target.transform as RectTransform;
                var childCount = rectTransform.childCount;
                if (childCount < 1) return;

                var anchorStepY = 1f / childCount;

                for (int i = childCount - 1; i >= 0; i--)
                {
                    var child = rectTransform.GetChild(i) as RectTransform;

                    // assuming anchoring to bottom left
                    var j = (childCount - 1) - i;
                    child.anchorMin = new Vector2(0, anchorStepY * j);
                    child.anchorMax = new Vector2(1, anchorStepY * (j + 1));
                    child.pivot = Vector2.zero;
                    child.sizeDelta = Vector2.zero;

                    EditorUtility.SetDirty(child);
                }
            }
        }
    }
}
