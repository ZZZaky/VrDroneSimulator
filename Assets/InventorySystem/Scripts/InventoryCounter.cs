using UnityEngine;
using TMPro;

namespace InventorySystem
{
    public class InventoryCounter : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private TextMeshProUGUI countText;

        [SerializeField]
        private CustomSocket socket;

        #endregion

        #region Life Cycle

        private void Start()
        {
            countText.text = $"{socket.Count}";

            if (socket.Count == socket.MaxCount)
                countText.color = Color.red;
            else
                countText.color = Color.white;
        }

        private void OnEnable()
        {
            socket.OnSelectEntered += OnChangeCount;
            socket.OnSelectExited += OnChangeCount;
        }

        private void OnDisable()
        {
            socket.OnSelectEntered -= OnChangeCount;
            socket.OnSelectExited -= OnChangeCount;
        }

        #endregion

        #region Private Methods

        private void OnChangeCount()
        {
            countText.text = $"{socket.Count}";

            if (socket.Count == socket.MaxCount)
                countText.color = Color.red;
            else
                countText.color = Color.white;

        }

        #endregion
    }
}
