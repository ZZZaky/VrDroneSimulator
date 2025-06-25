using FDS.Drone.ControlModes;

namespace FDS.Drone.Inputs.ControlModes.Angles
{
    public abstract class AnglesControlMode : DroneControlMode
    {
        #region Public Methods

        public abstract DroneAngles GetTargetAngles(ControlModeInputData inputData);

        #endregion
    }
}
