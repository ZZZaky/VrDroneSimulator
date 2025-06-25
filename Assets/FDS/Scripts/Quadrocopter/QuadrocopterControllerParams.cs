using FDS.Drone.Components;
using UnityEngine;

namespace FDS.Quadrocopter
{
    [CreateAssetMenu(fileName = "QuadrocopterControllerParams", menuName = "Drone/Quadrocopter/ControllerParams")]
    public class QuadrocopterControllerParams : ScriptableObject
    {
        #region Fields

        [SerializeField]
        private float enginePowerDeltaLimit;

        [SerializeField]
        private PidController pitchPID;

        [SerializeField]
        private PidController rollPID;

        [SerializeField]
        private PidController yawPID;

        [Min(0)]
        [SerializeField]
        private float minThrottle;

        [Min(1)]
        [SerializeField]
        private float maxThrottle;

        #endregion

        #region Properties

        public float EnginePowerDeltaLimit { get => enginePowerDeltaLimit; set { enginePowerDeltaLimit = value; } }
        public PidController PitchPID => pitchPID;
        public PidController RollPID => rollPID;
        public PidController YawPID => yawPID;

        public float MaxThrottle { get => maxThrottle; set { maxThrottle = value; } }
        public float MinThrottle { get => minThrottle; set { minThrottle = value; } }

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            pitchPID.Reset();
            rollPID.Reset();
            yawPID.Reset();
        }

        #endregion
    }
}
