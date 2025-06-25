using UnityEngine;

namespace FDS.Drone.Components.EmergencySensors
{
    public abstract class EmergencySensor : MonoBehaviour
    {
        #region Public Fields

        public abstract bool IsEmergencySituation(Drone drone);

        #endregion
    }
}
