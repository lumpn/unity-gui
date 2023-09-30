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
                var rs = target.relativeSpacing;
                var anchorMin = Vector2.zero + new Vector2(rp.left, rp.bottom);
                var anchorMax = Vector2.one - new Vector2(rp.right, rp.top);
                var anchorStepY = (1f - rp.top - rp.bottom + rs) / childCount;
                var anchorSpaceY = (1f - rp.top - rp.bottom - (rs * (childCount - 1))) / childCount;

                var tap = target.absolutePadding;
                var tas = target.absoluteSpacing;

                var sdY = (tap.top + tap.bottom + tas * (childCount - 1)) / childCount;
                var sd = new Vector2(tap.left + tap.right, sdY);

                for (int i = childCount - 1; i >= 0; i--)
                {
                    var child = rectTransform.GetChild(i) as RectTransform;

                    var j = (childCount - 1) - i;
                    child.anchorMin = new Vector2(anchorMin.x, anchorMin.y + anchorStepY * j);
                    child.anchorMax = new Vector2(anchorMax.x, anchorMin.y + anchorStepY * j + anchorSpaceY);

                    child.anchoredPosition = new Vector2((tap.left - tap.right) / 2, tap.bottom - sdY * (j + 0.5f) + tas * j);
                    child.sizeDelta = -sd;
                    child.pivot = new Vector2(0.5f, 0.5f);

                    EditorUtility.SetDirty(child);
                }
            }
        }
    }
}
