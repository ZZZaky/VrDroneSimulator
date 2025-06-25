using FDS.Drone.Components;
using TMPro;
using UnityEngine;

namespace FDS.UI.Indicators.Battery
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DroneBatteryIndicator : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        protected DroneBattery battery;

        [SerializeField] protected Color lowBatteryColor;

        protected TextMeshProUGUI _label;
        protected Color _defaultColor;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();
            _defaultColor = _label.color;
        }

        #endregion
    }
}
