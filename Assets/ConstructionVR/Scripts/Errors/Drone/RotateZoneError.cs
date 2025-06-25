using ConstructionVR.CheckerUtilitis;
using UnityEngine;

namespace ConstructionVR.Errors
{
    public class RotateZoneError : Error
    {
        #region Filds

        [SerializeField]
        private RotateType rotateType;

        private ZoneDetection _zoneDetection;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _zoneDetection = FindObjectOfType<ZoneDetection>();

            if (_zoneDetection == null)
            {
                Debug.LogError("Zone detection not found!");
            }
        }

        #endregion

        #region PublicMethods

        public override bool TryGetError(out string errorMessage)
        {
            errorMessage = "";

            var currentZone = _zoneDetection.GetZoneFromPostion(transform.position);
            var relationZoneCW = (currentZone == ZoneDetection.Zone.Three || currentZone == ZoneDetection.Zone.Four);

            if (relationZoneCW)
            {
                if (rotateType == RotateType.CW)
                    return false;
            }
            else
            {
                if (rotateType == RotateType.CCW)
                    return false;
            }

            errorMessage = GetErrorMessage() + currentZone.ToString();
            return true;
        }

        public virtual string GetErrorMessage()
        {
            return "Ошибка установки детали в зоне номер ";
        }

        #endregion
    }
}