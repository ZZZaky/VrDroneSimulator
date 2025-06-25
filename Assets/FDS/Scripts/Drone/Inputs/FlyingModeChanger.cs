using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using FDS.Drone.Inputs.ControlModes.Angles;
using FDS.Drone.Inputs.ControlModes.Throttle;
using UnityEngine.InputSystem;
using FDS.Interfaces;
using UnityEngine.Windows;

namespace FDS.Drone.Inputs
{
    [RequireComponent(typeof(PlayerInput))]
    public class FlyingModeChanger : MonoBehaviour, IResetable
    {
        #region Fields

        [SerializeField]
        private DroneManagement droneManagement;

        [SerializeField]
        private TMP_Dropdown presetsDropdown;

        [SerializeField]
        private List<FlyingModePreset> presets;


        #endregion

        #region LifeCycle

        private void Awake()
        {
            OnModeChanged(0);
        }
        private void OnEnable()
        {
            presetsDropdown.onValueChanged.AddListener(OnModeChanged);
        }
        private void OnDisable()
        {
            presetsDropdown.onValueChanged.RemoveListener(OnModeChanged);
        }

        #endregion

        #region Private Methods

        private void OnModeChanged(int value)
        {
            droneManagement.ChangeAngleControlMode(presets[value].Angles);
            droneManagement.ChangeThrottleControlMode(presets[value].Throttle);
        }

        private void OnChangeMode(InputValue input)
        {
            OpenChangeFlyMode((int)input.Get<float>());
        }

        void IResetable.Reset()
        {
            presetsDropdown.value = 0;
            OnModeChanged(0);
        }

        #endregion

        #region Public Methods

        public void OpenChangeFlyMode(int val)
        {
            var value = val + presetsDropdown.value;
            if (value < 0)
                value += presetsDropdown.options.Count;
            value %= presetsDropdown.options.Count;
            presetsDropdown.value = value;
            OnModeChanged(value);
        }

        #endregion
    }
}
