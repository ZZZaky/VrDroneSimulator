using UnityEngine;

namespace FDS.Drone.Inputs.ControlModes.Throttle
{
    [CreateAssetMenu(fileName = "AltitudeThrottleMode", menuName = "Drone/Inputs/FlyingSettings/Throttle/AltitudeThrottleMode")]
    class AltitudeThrottleControlModeSettings : DirectThrottleControlModeSettings
    {
        #region Fields

        [SerializeField]
        private float distanceCoefficient;

        #endregion

        #region Properties

        public float DistanceCoefficient { get => distanceCoefficient; set => distanceCoefficient = value; }

        #endregion
    }
}
