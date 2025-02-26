using UnityEngine;
using UnityEngine.EventSystems;
using UnityButton = UnityEngine.UI.Button;

namespace Lumpn.UGUI
{
    [AddComponentMenu("UI/Button - Lumpn")]
    public sealed class Button : UnityButton
    {
        private bool isPointerInside;

        protected override void InstantClearState()
        {
            isPointerInside = false;
            base.InstantClearState();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            isPointerInside = true;
            if (Cursor.visible)
            {
                base.OnPointerEnter(eventData);
            }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            isPointerInside = false;
            base.OnPointerExit(eventData);
        }

        private void Evaluate(bool isCursorVisible)
        {
            if (isPointerInside && isCursorVisible)
            {
                base.OnPointerEnter(null);
            }
            else
            {
                base.OnPointerExit(null);
            }
        }

        public static void EvaluateAll()
        {
            var isCursorVisible = Cursor.visible;
            for (int i = 0; i < s_SelectableCount; i++)
            {
                if (s_Selectables[i] is Button button)
                {
                    button.Evaluate(isCursorVisible);
                }
            }
        }
    }
}
