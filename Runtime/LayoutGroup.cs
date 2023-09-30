//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.UGUI
{
    public sealed class LayoutGroup : MonoBehaviour
    {
        [System.Serializable]
        public struct RectOffset
        {
            [SerializeField] float _left, _right, _top, _bottom;

            public float left => _left;
            public float right => _right;
            public float top => _top;
            public float bottom => _bottom;
        }

        [SerializeField] private RectOffset _relativePadding;
        [SerializeField] private float _relativeSpacing;

        [SerializeField] private RectOffset _absolutePadding;
        [SerializeField] private float _absoluteSpacing;

        public RectOffset relativePadding => _relativePadding;
        public float relativeSpacing => _relativeSpacing;

        public RectOffset absolutePadding => _absolutePadding;
        public float absoluteSpacing => _absoluteSpacing;
    }
}
