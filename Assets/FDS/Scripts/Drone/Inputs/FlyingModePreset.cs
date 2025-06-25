using FDS.Drone.Inputs.ControlModes.Angles;
using FDS.Drone.Inputs.ControlModes.Throttle;
using System;

namespace FDS.Drone.Inputs
{
    [Serializable]
    public struct FlyingModePreset
    {
        #region Fields

        public AnglesControlMode Angles;
        public ThrottleControlMode Throttle;

        #endregion
    }
}
