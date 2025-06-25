using UnityEngine;

namespace FDS.Drone.ControlModes
{
    public abstract class DroneControlMode : MonoBehaviour
    {
        #region Public Methods

        public abstract void OnSelect();

        #endregion
    }
}
