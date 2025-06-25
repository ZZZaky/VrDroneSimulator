using UnityEngine;

namespace FDS.Drone.Inputs.Converters
{
    public class KeyboardThrottleInputConverter : DroneInputConverter
    {
        #region Fields

        [SerializeField]
        private float sensitivity;

        [SerializeField]
        private float returnSpeed;

        private float _value;

        #endregion

        #region Public Methods

        public override float GetConvertedAxisValue(float inputAxis)
        {
            if (inputAxis == 0)
                _value = Mathf.Lerp(_value, 0.5f, Time.deltaTime * returnSpeed);
            else
                _value = Mathf.Clamp(_value + inputAxis * Time.deltaTime * sensitivity, 0, 1);
            Debug.Log(_value);

            return _value;
        }

        #endregion
    }
}
