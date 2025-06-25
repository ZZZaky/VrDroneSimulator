using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventoryBackground : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private CustomSocket socket;

        [SerializeField]
        private Image image;

        [SerializeField]
        private Color selectedColor;

        [SerializeField]
        private Color emptyColor;

        [SerializeField]
        private float hoverAlpha = 0.8f;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            if (socket == null)
                socket = transform.parent.GetComponentInChildren<CustomSocket>();
        }

        private void OnEnable()
        {
            socket.OnHoverEntered += OnItemPlacing;
            socket.OnHoverExited += OnItemExtracting;
            socket.OnSelectEntered += OnChangeSelected;
            socket.OnSelectExited += OnChangeSelected;
        }


        private void OnDisable()
        {
            socket.OnHoverEntered -= OnItemPlacing;
            socket.OnHoverExited -= OnItemExtracting;
            socket.OnSelectEntered -= OnChangeSelected;
            socket.OnSelectExited -= OnChangeSelected;
        }

        #endregion

        #region Private Methods

        private void OnChangeSelected()
        {
            if (socket.Count == 0)
                image.color = emptyColor;
            else
                image.color = selectedColor;
        }

        private void OnItemPlacing()
        {
            Color hoverColor = image.color;
            hoverColor.a = hoverAlpha;

            image.color = hoverColor;
        }

        private void OnItemExtracting()
        {
            Color hoverColor = image.color;
            hoverColor.a = selectedColor.a;

            image.color = hoverColor;
        }

        #endregion
    }
}
