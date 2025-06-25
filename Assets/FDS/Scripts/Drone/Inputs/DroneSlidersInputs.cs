using FDS.UI;
using UnityEngine;
using UnityEngine.UI;

namespace FDS.Drone.Inputs
{
    public class DroneSlidersInputs : DroneInputs
    {
        #region Fields

        [SerializeField]
        private DroneManagement droneManagement;

        [Header("Sliders")]
        [SerializeField]
        private NormalizedSlider upAxisSlider;

        [SerializeField]
        private Slider rightAxisSlider;

        [SerializeField]
        private Slider forwardAxisSlider;

        [SerializeField]
        private Slider rotateAxisSlider;

        #endregion

        #region Public Methods

        public override float GetForwardAxisInput()
        {
            var value = forwardAxisSlider.value;
            CheckForwardRightAxisValue(value);
            return value;
        }

        public override float GetRightAxisInput()
        {
            var value = rightAxisSlider.value;
            CheckForwardRightAxisValue(value);

            return value;
        }

        public override float GetRotationAxisInput()
        {
            var value = rotateAxisSlider.value;
            CheckForwardRightAxisValue(value);
            return value;
        }

        public override float GetUpAxisInput()
        {
            var value = upAxisSlider.NormalizedValue;
            CheckUpRightAxisValue(value);
            return value;
        }

        #endregion

        #region Private Methods

        private void CheckForwardRightAxisValue(float value)
        {
            if (value > 1 || value < -1)
                Debug.LogError($"{gameObject.name}:  Значение {value} оси управления должно быть в пределах [-1; 1]");
        }

        private void CheckUpRightAxisValue(float value)
        {
            if (value > 1 || value < 0)
                Debug.LogError($"{gameObject.name}:  Значение {value} оси управления должно быть в пределах [0; 1]");
        }

        #endregion
    }
}
