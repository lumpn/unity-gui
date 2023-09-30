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

            if (GUILayout.Button("Horizontal"))
            {
                ApplyHorizontal(target);
            }

            if (GUILayout.Button("Vertical"))
            {
                ApplyVertical(target);
            }

            EditorGUILayout.EndHorizontal();
        }

        public static void ApplyHorizontal(LayoutGroup layoutGroup)
        {
            var rectTransform = layoutGroup.transform as RectTransform;
            var childCount = rectTransform.childCount;
            if (childCount < 1) return;

            var rp = layoutGroup.relativePadding;
            var rs = layoutGroup.relativeSpacing;
            var anchorMin = Vector2.zero + new Vector2(rp.left, rp.bottom);
            var anchorMax = Vector2.one - new Vector2(rp.right, rp.top);
            var anchorStepX = (1f - rp.left - rp.right + rs) / childCount;
            var anchorSpaceX = (1f - rp.left - rp.right - (rs * (childCount - 1))) / childCount;

            var tap = layoutGroup.absolutePadding;
            var tas = layoutGroup.absoluteSpacing;

            var sdX = (tap.left + tap.right + tas * (childCount - 1)) / childCount;
            var sd = new Vector2(sdX, tap.top + tap.bottom);

            for (int i = childCount - 1; i >= 0; i--)
            {
                var child = rectTransform.GetChild(i) as RectTransform;

                var j = (childCount - 1) - i;
                child.anchorMin = new Vector2(anchorMin.x + anchorStepX * j, anchorMin.y);
                child.anchorMax = new Vector2(anchorMin.x + anchorStepX * j + anchorSpaceX, anchorMax.y);

                child.anchoredPosition = new Vector2(tap.left - sdX * (j + 0.5f) + tas * j, (tap.bottom - tap.top) / 2);
                child.sizeDelta = -sd;
                child.pivot = new Vector2(0.5f, 0.5f);

                EditorUtility.SetDirty(child);
            }
        }

        public static void ApplyVertical(LayoutGroup layoutGroup)
        {
            var rectTransform = layoutGroup.transform as RectTransform;
            var childCount = rectTransform.childCount;
            if (childCount < 1) return;

            var rp = layoutGroup.relativePadding;
            var rs = layoutGroup.relativeSpacing;
            var anchorMin = Vector2.zero + new Vector2(rp.left, rp.bottom);
            var anchorMax = Vector2.one - new Vector2(rp.right, rp.top);
            var anchorStepY = (1f - rp.top - rp.bottom + rs) / childCount;
            var anchorSpaceY = (1f - rp.top - rp.bottom - (rs * (childCount - 1))) / childCount;

            var tap = layoutGroup.absolutePadding;
            var tas = layoutGroup.absoluteSpacing;

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
