using UnityEngine;

namespace FDS.Drone.Inputs.ControlModes.Throttle
{
    public class ThrottleControlModeSettings : ScriptableObject
    {
        #region Fields

        [SerializeField]
        private float velocityCoefficient;

        #endregion

        #region Properties

        public float VelocityCoefficient { get => velocityCoefficient; set => velocityCoefficient = value; }

        #endregion
    }
}
