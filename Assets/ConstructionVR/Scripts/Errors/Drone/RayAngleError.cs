using ConstructionVR.SubInteractableSystem.RotatorGrab;
using UnityEngine;

namespace ConstructionVR.Errors
{
    public class RayAngleError : Error
    {
        #region Fields

        [SerializeField]
        private Rotator rotator;

        [SerializeField, Range(0, 180)]
        private int maxShiftAngle = 4;

        #endregion

        #region PublicMethods

        public override bool TryGetError(out string errorMessage)
        {
            var angle = rotator.GetCurrentEulerAngle().z;

            if (angle > 180)
                angle = 360 - angle;

            angle = Mathf.Abs(angle);

            if (angle >= maxShiftAngle)
            {
                errorMessage = "Ошибка в установке угла луча";
                return true;
            }

            errorMessage = "";
            return false;
        }

        #endregion
    }
}
