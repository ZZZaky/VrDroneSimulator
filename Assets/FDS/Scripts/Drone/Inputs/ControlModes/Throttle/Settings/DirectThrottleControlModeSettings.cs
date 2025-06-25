using UnityEngine;

namespace FDS.Drone.Inputs.ControlModes.Throttle
{
    [CreateAssetMenu(fileName = "DirectThrottleMode", menuName = "Drone/Inputs/FlyingSettings/Throttle/DirectThrottleMode")]
    class DirectThrottleControlModeSettings : ThrottleControlModeSettings
    {
        #region Fields

        [SerializeField]
        private float throttleChangingSpeed;

        [SerializeField]
        [Range(0, 1)]
        private float enableSpeedResetInterval;


        #endregion

        #region Properties

        public float ThrottleChangingSpeed { get => throttleChangingSpeed; set => throttleChangingSpeed = value; }
        public float EnableSpeedResetInterval { get => enableSpeedResetInterval; set => enableSpeedResetInterval = value; }

        #endregion
    }
}
