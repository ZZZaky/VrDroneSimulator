using UnityEngine;

namespace FDS.Drone.Components
{
    public class SignalPowerProvider : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Drone drone;

        [SerializeField]
        private float interferencesStartDistance;

        [SerializeField]
        protected float maxSignalDistance;

        #endregion

        #region Properties

        protected float interferencesWidth { get; private set; }

        #endregion

        #region LifeCycle

        private void Start()
        {
            interferencesWidth = maxSignalDistance - interferencesStartDistance;
        }

        #endregion

        #region Public Methods

        public virtual float GetSignalPower()
        {
            var dronePos = drone.BodyTransform.position;
            var droneDistanceFromHome = (drone.Home - dronePos).magnitude;

            if (droneDistanceFromHome < interferencesStartDistance)
                return 1;
            else if (droneDistanceFromHome >= maxSignalDistance)
                return 0;

            return 1 - (droneDistanceFromHome - interferencesStartDistance) / interferencesWidth;
        }

        #endregion
    }
}