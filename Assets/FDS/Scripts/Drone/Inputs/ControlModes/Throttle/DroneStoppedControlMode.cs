using FDS.Drone.ControlModes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDS.Drone.Inputs.ControlModes.Throttle
{
    class DroneStoppedControlMode : ThrottleControlMode
    {
        #region Public Methods

        public override float GetThrottle(ControlModeInputData inputData)
        {
            return 0;
        }

        public override void OnSelect()
        {
        }

        #endregion
    }
}
