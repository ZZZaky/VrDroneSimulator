using UnityEngine;

namespace FDS.Drone.Inputs.Converters
{
    public abstract class DroneInputConverter : MonoBehaviour
    {
        #region Public Methods

        public abstract float GetConvertedAxisValue(float inputAxis);

        #endregion
    }
}
