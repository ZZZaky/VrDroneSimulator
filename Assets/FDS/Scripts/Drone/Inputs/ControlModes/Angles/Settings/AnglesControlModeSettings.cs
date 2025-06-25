using UnityEngine;

namespace FDS.Drone.Inputs.ControlModes.Angles.Settings
{
    public class AnglesControlModeSettings : ScriptableObject
    {
        #region Fields

        [SerializeField]
        private float yawChangingSpeed;

        [SerializeField]
        private float velocityCoefficient;

        #endregion

        #region Properties

        public float YawChangingSpeed { get => yawChangingSpeed; set => yawChangingSpeed = value; }
        public float VelocityCoefficient { get => velocityCoefficient; set => velocityCoefficient = value; }

        #endregion
    }
}
