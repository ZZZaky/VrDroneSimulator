using ConstructionVR.Errors.UI;
using System.Collections.Generic;
using UnityEngine;

namespace ConstructionVR.Errors
{
    public class ErrorManager : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private ShowAssemblyInformatioBar informatioBar;

        [SerializeField]
        private ErrorMessageBox messageErrorBox;

        [SerializeField]
        private ErrorLableBox lableWorldErrorBoxPrefab;

        private Error[] errors;

        List<GameObject> controlErrors = new List<GameObject>();

        #endregion

        #region LifeCycle

        private void Awake()
        {
            errors = FindObjectsOfType<Error>();
        }

        #endregion

        #region PublicMethods

        public void CheckErrors()
        {
            var countErrors = 0;
            List<ErrorSruct> errorsMessage = new List<ErrorSruct>(errors.Length);

            Dictionary<Transform, List<ErrorSruct>> erorrsWorldLabel = new();

            foreach( GameObject objToDelete in controlErrors)
            {
                Destroy(objToDelete);
            }

            controlErrors.Clear();

            foreach (Error error in errors)
            {
                string messsageError;

                if (error.TryGetError(out messsageError))
                {
                    countErrors++;
                    var errorStruct = new ErrorSruct { Message = messsageError, ErrorCode = countErrors };
                    errorsMessage.Add(errorStruct);

                    if (!erorrsWorldLabel.ContainsKey(error.Offset))
                    {
                        erorrsWorldLabel.Add(error.Offset, new());
                    }

                    erorrsWorldLabel[error.Offset].Add(errorStruct);
                }
            }

            var generalMessage = countErrors == 0 ? $"Допущено ошибок {countErrors} из {errors.Length}. \n " +
                $"Поздравляю, сборка успешно завершена !" : $"Допущено ошибок {countErrors} из {errors.Length}. \n " +
                $"В процентном соотношении: {((countErrors / (float)errors.Length) * 100).ToString("f0")} % \n" +
                "Были допущены следующие ошибки:";

            ShowWorldErrorLabel(erorrsWorldLabel);
            informatioBar.ShowMessageErrors();
            messageErrorBox.ShowErrors(generalMessage, errorsMessage);
        }

        private void ShowWorldErrorLabel(Dictionary<Transform, List<ErrorSruct>> erorrsWorldLabel)
        {
            foreach (var errorsInLabel in erorrsWorldLabel)
            {
                var worlErrLabel = Instantiate(lableWorldErrorBoxPrefab, Vector3.zero, Quaternion.identity);
                worlErrLabel.ShowLable(errorsInLabel.Key.position, errorsInLabel.Value);
                controlErrors.Add(worlErrLabel.gameObject);
            }
        }

        #endregion
    }
}