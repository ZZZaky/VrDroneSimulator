using UnityEngine;

namespace FDS.Drone.Inputs.ControlModes.Angles.Settings
{
    [CreateAssetMenu(fileName = "DirectAnglesMode", menuName = "Drone/Inputs/FlyingSettings/Angles/DirectAnglesMode")]
    class DirectAnglesControlModeSettings : AnglesControlModeSettings
    {
        #region Fields

        [SerializeField]
        private float forwardSpeed;

        [SerializeField]
        private float rightSpeed;


        #endregion

        #region Properties

        public float ForwardSpeed { get => forwardSpeed; set => forwardSpeed = value; }
        public float RightSpeed { get => rightSpeed; set => rightSpeed = value; }

        #endregion
    }
}
