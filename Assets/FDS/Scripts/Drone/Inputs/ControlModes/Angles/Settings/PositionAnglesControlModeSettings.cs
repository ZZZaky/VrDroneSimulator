using UnityEngine;

namespace FDS.Drone.Inputs.ControlModes.Angles.Settings
{
    [CreateAssetMenu(fileName = "PositionAnglesMode", menuName = "Drone/Inputs/FlyingSettings/Angles/PositionAnglesMode")]
    class PositionAnglesControlModeSettings : DirectAnglesControlModeSettings
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
