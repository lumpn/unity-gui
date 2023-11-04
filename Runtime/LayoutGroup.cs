//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lumpn.UGUI
{
    public sealed class LayoutGroup : MonoBehaviour
    {
        [System.Serializable]
        private struct RectOffset
        {
            [SerializeField] public float left, right, top, bottom;
        }

        [SerializeField] private RectOffset relativePadding;
        [SerializeField] private float relativeSpacing;

        [SerializeField] private RectOffset absolutePadding;
        [SerializeField] private float absoluteSpacing;

        [SerializeField] private Vector2 childSize;

        public void FitHorizontal()
        {
            var rectTransform = (RectTransform)transform;
            var childCount = GetChildren(rectTransform).Count();

            var size = childCount * childSize.x;
            size += absolutePadding.left;
            size += absolutePadding.right;
            size += absoluteSpacing * Mathf.Max(childCount - 1, 0);

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        }

        public void FitVertical()
        {
            var rectTransform = (RectTransform)transform;
            var childCount = GetChildren(rectTransform).Count();

            var size = childCount * childSize.y;
            size += absolutePadding.top;
            size += absolutePadding.bottom;
            size += absoluteSpacing * Mathf.Max(childCount - 1, 0);

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        }

        public void ApplyHorizontal()
        {
            var children = GetChildren(transform).ToArray();
            var childCount = children.Length;
            if (childCount < 1) return;

            var rp = relativePadding;
            var rs = relativeSpacing;
            var anchorMin = Vector2.zero + new Vector2(rp.left, rp.bottom);
            var anchorMax = Vector2.one - new Vector2(rp.right, rp.top);
            var anchorStepX = (1f - rp.left - rp.right + rs) / childCount;
            var anchorSpaceX = (1f - rp.left - rp.right - (rs * (childCount - 1))) / childCount;

            var tap = absolutePadding;
            var tas = absoluteSpacing;

            var sdX = (tap.left + tap.right + tas * (childCount - 1)) / childCount;
            var sd = new Vector2(sdX, tap.top + tap.bottom);

            for (int i = childCount - 1; i >= 0; i--)
            {
                var child = children[i];

                var j = (childCount - 1) - i;
                child.anchorMin = new Vector2(anchorMin.x + anchorStepX * j, anchorMin.y);
                child.anchorMax = new Vector2(anchorMin.x + anchorStepX * j + anchorSpaceX, anchorMax.y);

                child.anchoredPosition = new Vector2(tap.left - sdX * (j + 0.5f) + tas * j, (tap.bottom - tap.top) / 2);
                child.sizeDelta = -sd;
            }
        }

        public void ApplyVertical()
        {
            var children = GetChildren(transform).ToArray();
            var childCount = children.Length;
            if (childCount < 1) return;

            var rp = relativePadding;
            var rs = relativeSpacing;
            var anchorMin = Vector2.zero + new Vector2(rp.left, rp.bottom);
            var anchorMax = Vector2.one - new Vector2(rp.right, rp.top);
            var anchorStepY = (1f - rp.top - rp.bottom + rs) / childCount;
            var anchorSpaceY = (1f - rp.top - rp.bottom - (rs * (childCount - 1))) / childCount;

            var tap = absolutePadding;
            var tas = absoluteSpacing;

            var sdY = (tap.top + tap.bottom + tas * (childCount - 1)) / childCount;
            var sd = new Vector2(tap.left + tap.right, sdY);

            for (int i = childCount - 1; i >= 0; i--)
            {
                var child = children[i];

                var j = (childCount - 1) - i;
                child.anchorMin = new Vector2(anchorMin.x, anchorMin.y + anchorStepY * j);
                child.anchorMax = new Vector2(anchorMax.x, anchorMin.y + anchorStepY * j + anchorSpaceY);

                child.anchoredPosition = new Vector2((tap.left - tap.right) / 2, tap.bottom - sdY * (j + 0.5f) + tas * j);
                child.sizeDelta = -sd;
            }
        }

        private static IEnumerable<RectTransform> GetChildren(Transform parent)
        {
            return parent.Cast<RectTransform>().Where(ObservesLayout);
        }

        private static bool ObservesLayout(Transform transform)
        {
            if (transform.TryGetComponent<ILayoutIgnorer>(out var ignorer))
            {
                return !ignorer.ignoreLayout;
            }
            return true;
        }
    }
}
