using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ConstructionVR.Errors.UI
{
    public class ErrorMessageBox : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private ErrorMessageContainer messageContainerPrefab;

        [SerializeField]
        private Transform errorScrollContent;

        [SerializeField]
        private TextMeshProUGUI generalMessageText;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            CleareErrors();
        }

        #endregion

        #region PublicMethods

        public void ShowErrors(string generalMessage, List<ErrorSruct> errors)
        {
            CleareErrors();

            generalMessageText.text = generalMessage;

            foreach (var error in errors)
            {
                var errorContainer = Instantiate(messageContainerPrefab, errorScrollContent);
                errorContainer.SetErrorMessage(error.ErrorCode, error.Message);
            }
        }

        #endregion

        #region PrivateMethods

        private void CleareErrors()
        {
            for (int i = 0; i < errorScrollContent.childCount; i++)
            {
                Destroy(errorScrollContent.GetChild(i).gameObject);
            }
        }

        #endregion
    }
}