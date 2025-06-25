using FDS.Drone.ControlModes;
using FDS.Drone.Inputs.ControlModes.Angles.Settings;
using UnityEngine;

namespace FDS.Drone.Inputs.ControlModes.Angles
{
    public class ForwardRightVelocityResetControlMode : AnglesControlMode
    {
        #region Fields

        [SerializeField]
        protected Drone drone;

        [SerializeField]
        protected Rigidbody droneBodyRb;

        [SerializeField]
        protected AnglesControlModeSettings controlModeSettings;
        #endregion

        #region Public Methods

        public override DroneAngles GetTargetAngles(ControlModeInputData inputData)
        {
            var newTargetAngles = inputData.TargetAngles;
            newTargetAngles.Yaw += Time.deltaTime * controlModeSettings.YawChangingSpeed * inputData.RotationDelta;

            var localVelocity = Quaternion.AngleAxis(-drone.ReadCurrentAngles().Yaw, Vector3.up) * droneBodyRb.velocity;

            var forwardBoost = GetForwardShift(inputData.ForwardDelta) - controlModeSettings.VelocityCoefficient * localVelocity.x;
            var leftBoost = GetRightShift(inputData.RightDelta) - controlModeSettings.VelocityCoefficient * localVelocity.z;

            forwardBoost *= Mathf.Sign(inputData.Throttle);
            leftBoost *= Mathf.Sign(inputData.Throttle);

            var pitch = Mathf.Atan2(-forwardBoost, -Physics.gravity.y);
            var roll = Mathf.Atan2(leftBoost * Mathf.Cos(pitch), -Physics.gravity.y);


            newTargetAngles.Pitch = pitch * Mathf.Rad2Deg;
            newTargetAngles.Roll = roll * Mathf.Rad2Deg;

            return newTargetAngles;
        }

        public virtual float GetForwardShift(float forwardDelta)
        {
            return 0;
        }
        public virtual float GetRightShift(float rightDelta)
        {
            return 0;
        }

        public override void OnSelect()
        {

        }

        #endregion
    }
}
