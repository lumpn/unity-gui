using UnityEngine;

namespace Lumpn.UGUI
{
    public sealed class VerticalLayoutGroup : MonoBehaviour
    {
        [System.Serializable]
        private struct RectOffset
        {
            [SerializeField] float left, right, top, bottom;
        }

        [SerializeField] private RectOffset padding;
        [SerializeField] private float spacing;

        public void Apply()
        {
            var rectTransform = transform as RectTransform;
            var childCount = rectTransform.childCount;
            if (childCount < 1) return;

            var anchorStepY = 1 / childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                var child = rectTransform.GetChild(i) as RectTransform;

                // assuming anchoring to bottom left
                var j = childCount - i;
                child.anchorMin = new Vector2(0, anchorStepY * j);
                child.anchorMax = new Vector2(1, anchorStepY * (j + 1));
                child.pivot = Vector2.zero;
            }
        }
    }
}
