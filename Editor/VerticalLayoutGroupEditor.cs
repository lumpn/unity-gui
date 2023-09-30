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

                var rp = target.relativePadding;
                var anchorMin = Vector2.zero + new Vector2(rp.left, rp.bottom);
                var anchorMax = Vector2.one - new Vector2(rp.right, rp.top);
                var anchorStepY = (1f - rp.top - rp.bottom) / childCount;

                for (int i = childCount - 1; i >= 0; i--)
                {
                    var child = rectTransform.GetChild(i) as RectTransform;

                    // assuming anchoring to bottom left
                    var j = (childCount - 1) - i;
                    child.anchorMin = new Vector2(anchorMin.x, anchorMin.y + anchorStepY * j);
                    child.anchorMax = new Vector2(anchorMax.x, anchorMin.y + anchorStepY * (j + 1));
                    child.anchoredPosition = Vector2.zero;
                    child.sizeDelta = Vector2.zero;
                    child.pivot = Vector2.zero;

                    EditorUtility.SetDirty(child);
                }
            }
        }
    }
}
