using FDS.Drone.Inputs;

namespace FDS.Drone.ControlModes
{
    public struct ControlModeInputData
    {
        #region Fields

        public float ForwardDelta;
        public float RightDelta;
        public float RotationDelta;
        public float UpDelta;
        public DroneAngles TargetAngles;
        public float Throttle;

        #endregion

        #region Constructors

        public ControlModeInputData(float forwardDelta, float rightDelta, float rotationDelta, float upDelta, DroneAngles targetAngles, float throttle)
        {
            ForwardDelta = forwardDelta;
            RightDelta = rightDelta;
            RotationDelta = rotationDelta;
            UpDelta = upDelta;
            TargetAngles = targetAngles;
            Throttle = throttle;
        }

        #endregion
    }
}
