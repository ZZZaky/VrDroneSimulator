using FDS.Drone.ControlModes;
using UnityEngine;

namespace FDS.Drone.Inputs.ControlModes.Throttle
{
    public class VerticalSpeedResetControlMode : ThrottleControlMode
    {
        #region Fields

        [SerializeField]
        protected float fullDroneMass;

        [SerializeField]
        protected int enginesCount;

        [SerializeField]
        protected Rigidbody droneBodyRb;

        [SerializeField]
        protected Drone drone;

        [SerializeField]
        protected ThrottleControlModeSettings controlModeSettings;

        #endregion

        #region Public Methods

        public override float GetThrottle(ControlModeInputData inputData)
        {
            var droneAngles = drone.ReadCurrentAngles();

            var force = fullDroneMass * (-Physics.gravity.y + GetShiftY(inputData.UpDelta) - controlModeSettings.VelocityCoefficient * droneBodyRb.velocity.y)
                / Mathf.Cos(droneAngles.Pitch * Mathf.Deg2Rad) / Mathf.Cos(droneAngles.Roll * Mathf.Deg2Rad);

            return force / enginesCount;
        }

        public virtual float GetShiftY(float upAxis)
        {
            return 0;
        }

        public override void OnSelect()
        {
        }

        #endregion
    }
}
