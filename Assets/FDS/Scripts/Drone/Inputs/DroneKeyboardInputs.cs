using UnityEngine;

namespace FDS.Drone.Inputs
{
    public class DroneKeyboardInputs : DroneInputs
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
            return _droneInput.Drone.ForwardVector2.ReadValue<Vector2>().y;
        }

        public override float GetRightAxisInput()
        {
            return _droneInput.Drone.RightVector2.ReadValue<Vector2>().x;
        }

        public override float GetUpAxisInput()
        {
            return _droneInput.Drone.UpVector2.ReadValue<Vector2>().y;
        }

        public override float GetRotationAxisInput()
        {
            return _droneInput.Drone.RotationVector2.ReadValue<Vector2>().x;
        }

        #endregion
    }
}
