using FDS.Drone.Inputs;
using UnityEngine;

namespace FDS.Quadrocopter
{
    public class QuadrocopterController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private QuadrocopterControllerParams controllerParams;

        [SerializeField]
        private DroneManagement droneManagement;

        #endregion

        #region Public Methods

        public QuadrocopterEnginesPower GetEnginesPower()
        {
            var throttle = Mathf.Clamp(droneManagement.Throttle, -controllerParams.MaxThrottle, controllerParams.MaxThrottle);
            if (Mathf.Abs(throttle) < controllerParams.MinThrottle)
                throttle = Mathf.Sign(throttle) * controllerParams.MinThrottle;

            var powerLimit = Mathf.Min(controllerParams.EnginePowerDeltaLimit, Mathf.Abs(throttle));
            var deltaPitch = NormalizeAngle(droneManagement.TargetAngles.Pitch - droneManagement.CurrentAngles.Pitch);
            var deltaYaw = NormalizeAngle(droneManagement.TargetAngles.Yaw - droneManagement.CurrentAngles.Yaw);
            var deltaRoll = NormalizeAngle(droneManagement.TargetAngles.Roll - droneManagement.CurrentAngles.Roll);

            var pitchForce = Mathf.Clamp((float)controllerParams.PitchPID.Calculate(0, deltaPitch / 180.0f), -powerLimit, powerLimit);
            var rollForce = Mathf.Clamp((float)controllerParams.RollPID.Calculate(0, deltaRoll / 180.0f), -powerLimit, powerLimit);
            var yawForce = Mathf.Clamp((float)controllerParams.YawPID.Calculate(0, deltaYaw / 180.0f), -powerLimit, powerLimit) * Mathf.Sign(throttle);

            var newMotorFRPower = (throttle + pitchForce + rollForce + yawForce);
            var newMotorBLPower = (throttle - pitchForce - rollForce + yawForce);
            var newMotorBRPower = (throttle - pitchForce + rollForce - yawForce);
            var newMotorFLPower = (throttle + pitchForce - rollForce - yawForce);

            return new QuadrocopterEnginesPower(newMotorFLPower, newMotorFRPower, newMotorBLPower, newMotorBRPower);
        }

        #endregion

        #region Private Methods

        private float NormalizeAngle(float angle)
        {
            var normalized = angle % 360;
            if (normalized < 0 && normalized < -180)
                normalized += 360;
            else if (normalized > 0 && normalized > 180)
                normalized -= 360;
            return normalized;
        }

        #endregion
    }
}
