using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Lumpn.UGUI
{
    public static class RectTransformEditorUtils
    {
        [MenuItem("CONTEXT/RectTransform/Match Pivot")]
        public static void MatchPivot(MenuCommand cmd)
        {
            var context = cmd.context;
            Debug.Log(context.GetType());

            if (context is RectTransform rectTransform)
            {
                var parent = rectTransform.parent as RectTransform;
                if (parent)
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
