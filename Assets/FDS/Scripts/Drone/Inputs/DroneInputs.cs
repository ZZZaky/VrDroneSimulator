using FDS.Drone.Inputs.Converters;
using UnityEngine;

namespace FDS.Drone.Inputs
{
    public abstract class DroneInputs : MonoBehaviour
    {
        [SerializeField]
        private DroneInputConverter upAxisConverter;

        [SerializeField]
        private DroneInputConverter forwardAxisConverter;

        [SerializeField]
        private DroneInputConverter rightAxisConverter;

        [SerializeField]
        private DroneInputConverter rotateAxisConverter;

        #region Public Methods

        public float GetForwardAxisConverted()
        {
            var forwardDelta = GetForwardAxisInput();

            if (forwardAxisConverter != null)
                return forwardAxisConverter.GetConvertedAxisValue(forwardDelta);
            return forwardDelta;
        }

        public float GetRightAxisConverted()
        {
            var rightDelta = GetRightAxisInput();

            if (rightAxisConverter != null)
                return rightAxisConverter.GetConvertedAxisValue(rightDelta);
            return rightDelta;
        }
        public float GetUpAxisConverted()
        {
            var upDelta = GetUpAxisInput();

            if (upAxisConverter != null)
                return upAxisConverter.GetConvertedAxisValue(upDelta);
            return upDelta;
        }

        public float GetRotationAxisConverted()
        {
            var rotateDelta = GetRotationAxisInput();

            if (rotateAxisConverter != null)
                return rotateAxisConverter.GetConvertedAxisValue(rotateDelta);
            return rotateDelta;
        }

        public abstract float GetForwardAxisInput();
        public abstract float GetRightAxisInput();
        public abstract float GetUpAxisInput();
        public abstract float GetRotationAxisInput();

        #endregion
    }
}
