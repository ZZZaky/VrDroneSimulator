using System.Collections.Generic;
using UnityEngine;

namespace FDS.Drone.Inputs.Converters
{
    public class DroneInputConvertersGroup : DroneInputConverter
    {
        #region Fields

        [SerializeField]
        private List<DroneInputConverter> converters;

        #endregion

        #region Public Methods

        public override float GetConvertedAxisValue(float inputAxis)
        {
            var value = inputAxis;
            foreach(var converter in converters)
                value = converter.GetConvertedAxisValue(value);
            return value;
        }

        #endregion
    }
}
