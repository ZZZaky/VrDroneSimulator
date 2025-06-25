using TMPro;
using UnityEngine;

namespace ConstructionVR.Errors.UI
{
    public class ErrorMessageContainer : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private CycleNumberContainer numberContainer;

        [SerializeField]
        private TextMeshProUGUI errorMessageText;

        #endregion

        #region PublicMethods

        public void SetErrorMessage(int errorIndex, string errorMessage)
        {
            numberContainer.TextMeshPro.text = errorIndex.ToString();
            errorMessageText.text = errorMessage;
        }

        #endregion
    }
}