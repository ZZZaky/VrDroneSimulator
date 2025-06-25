using UnityEngine;

namespace FDS.Drone.Inputs
{
    public class DroneControllerInputs : DroneInputs
    {
        #region Fields

        private DronePlayerInput _droneInput;

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            if (_droneInput == null)
                _droneInput = new DronePlayerInput();
            _droneInput.Enable();
        }

        private void OnDisable()
        {
            _droneInput.Disable();
        }

        #endregion

        #region Public Methods

        public override float GetForwardAxisInput()
        {
            return _droneInput.Drone.ForwardAxis.ReadValue<float>();
        }

        public override float GetRightAxisInput()
        {
            return _droneInput.Drone.RightAxis.ReadValue<float>();
        }

        public override float GetUpAxisInput()
        {
            Debug.Log(_droneInput.Drone.UpAxis.ReadValue<float>());
            return _droneInput.Drone.UpAxis.ReadValue<float>();
        }

        public override float GetRotationAxisInput()
        {
            return _droneInput.Drone.RotationAxis.ReadValue<float>();
        }

        #endregion
    }
}
