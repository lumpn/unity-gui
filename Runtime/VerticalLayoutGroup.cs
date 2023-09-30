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
            // TODO Jonas: do the work
        }
    }
}
