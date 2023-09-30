//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEditor;
using UnityEngine;

namespace Lumpn.UGUI
{
    public static class RectTransformEditorUtils
    {
        [MenuItem("CONTEXT/RectTransform/Match Pivot")]
        public static void MatchPivot(MenuCommand cmd)
        {
            if (cmd.context is RectTransform rectTransform)
            {
                if (rectTransform.parent is RectTransform parent)
                {
                    var pivot = parent.pivot;
                    rectTransform.anchorMin = pivot;
                    rectTransform.anchorMax = pivot;
                    EditorUtility.SetDirty(rectTransform);
                }
            }
        }
    }
}
