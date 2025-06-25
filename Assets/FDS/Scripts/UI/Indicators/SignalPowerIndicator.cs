using FDS.Drone.Components;
using TMPro;
using UnityEngine;

namespace FDS.UI.Indicators
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SignalPowerIndicator : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private SignalPowerProvider signalPowerProvider;

        private TextMeshProUGUI _label;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _label.text = $"{Mathf.Ceil(signalPowerProvider.GetSignalPower() * 100)}%";
        }

        #endregion
    }
}