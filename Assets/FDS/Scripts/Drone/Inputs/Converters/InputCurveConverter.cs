using UnityEngine;

namespace FDS.Drone.Inputs.Converters
{
    public class InputCurveConverter : DroneInputConverter
    {
        #region Fields

        [SerializeField]
        private AnimationCurve curve;

        #endregion

        #region Public Methods

        public override float GetConvertedAxisValue(float inputAxis)
        {
            return curve.Evaluate(inputAxis);
        }

        #endregion
    }
}
