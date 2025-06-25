using TMPro;
using UnityEngine;

namespace FDS.UI.Indicators
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DronePropertyIndicator : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        protected Drone.Drone drone;

        protected TextMeshProUGUI _label;

        #endregion

        #region LifeCylce

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();
        }

        #endregion
    }
}
