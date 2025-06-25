using UnityEngine;

namespace FDS.Drone.Components.EmergencySensors
{
    public class AnglesEmergencySensor : EmergencySensor
    {
        #region Fields

        [SerializeField]
        [Range(0, 90)]
        private float maximumAngleDiviation;

        #endregion

        #region Public Methods

        public override bool IsEmergencySituation(Drone drone)
        {
            var angles = drone.ReadCurrentAngles();
            return IsAngleEmergency(angles.Pitch) || IsAngleEmergency(angles.Roll);
        }

        #endregion

        #region Private Methods

        private bool IsAngleEmergency(float angle)
        {
            return angle > maximumAngleDiviation && angle <= 360 - maximumAngleDiviation;
        }

        #endregion
    }
}
