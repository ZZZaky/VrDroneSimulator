using TMPro;
using UnityEngine;

namespace ConstructionVR.Errors.UI
{
    public class CycleNumberContainer : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private TextMeshProUGUI m_TextMeshProUGUI;

        #endregion

        #region Properties

        public TextMeshProUGUI TextMeshPro => m_TextMeshProUGUI;

        #endregion
    }
}