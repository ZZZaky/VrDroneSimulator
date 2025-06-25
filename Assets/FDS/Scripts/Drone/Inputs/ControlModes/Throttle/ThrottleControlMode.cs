using FDS.Drone.ControlModes;

namespace FDS.Drone.Inputs.ControlModes.Throttle
{
    public abstract class ThrottleControlMode : DroneControlMode
    {
        #region Public Methods

        public abstract float GetThrottle(ControlModeInputData inputData);

        #endregion
    }
}
