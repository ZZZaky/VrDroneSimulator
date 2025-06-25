using System.Collections.Generic;
using UnityEngine;

namespace ConstructionVR.Errors.UI
{
    public class ErrorLableBox : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private CycleNumberContainer numberContainerPrefab;

        [SerializeField]
        private Transform numberContainerContent;

        [SerializeField]
        private Transform offset;

        private List<CycleNumberContainer> errorNumbersPool;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            errorNumbersPool = new List<CycleNumberContainer>(numberContainerContent.childCount);

            for (int i = 0; i < numberContainerContent.childCount; i++)
            {
                if (numberContainerContent.GetChild(i).TryGetComponent<CycleNumberContainer>(out var numberContainer))
                {
                    errorNumbersPool.Add(numberContainer);
                    numberContainer.gameObject.SetActive(false);
                }
                else
                {
                    Destroy(numberContainerContent.GetChild(i));
                }
            }
        }

        #endregion

        #region PublicMethods

        public void ShowLable(Vector3 worldPosition, List<ErrorSruct> errorNumbers)
        {
            offset.position = worldPosition;
            ShowErrorNumbers(errorNumbers);
        }

        #endregion

        #region PrivateMethods

        private void ShowErrorNumbers(List<ErrorSruct> errorNumbers)
        {
            if (errorNumbers.Count > errorNumbersPool.Count)
            {
                CreateErrorNumberContainers(errorNumbers.Count - errorNumbersPool.Count);
            }

            foreach (var container in errorNumbersPool)
            {
                container.gameObject.SetActive(false);
            }

            for (int i = 0; i < errorNumbers.Count; i++)
            {
                errorNumbersPool[i].gameObject.SetActive(true);
                errorNumbersPool[i].TextMeshPro.text = errorNumbers[i].ErrorCode.ToString();
            }
        }

        private void CreateErrorNumberContainers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var container = Instantiate(numberContainerPrefab, numberContainerContent);
                errorNumbersPool.Add(container);
                container.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}