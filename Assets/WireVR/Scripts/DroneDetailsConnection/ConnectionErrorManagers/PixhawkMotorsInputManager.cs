using ConstructionVR.CheckerUtilitis;
using UnityEngine;

namespace WireVR.Connection
{
    public class PixhawkMotorsInputManager : InputManager
    {
        #region Fields

        [SerializeField]
        private ZoneDetection zoneDetection;

        #endregion

        #region Public Methods

        public override bool TryGetError(out string errorMessage)
        {
            if (input.CurrentWire  == null || !input.CurrentWire.IsConnected)
            {
                errorMessage = ErrorMessages.notConnectedErrorMessage;
                return true;
            }

            if (!(input.CurrentWire.Data is MotorsInputConfig))
            {
                errorMessage = ErrorMessages.invalidDetailErrorMessage;
                return true;
            }

            var inputConfig = (MotorsInputConfig)input.CurrentWire.Data;

            if (input.AllowDetail != inputConfig.detail)
            {
                errorMessage = ErrorMessages.invalidDetailErrorMessage;
                return true;
            }

            var inputZone = input.GetComponent<MotorControllerZone>().Zone;
            var motorControllerZone = zoneDetection.GetZoneFromPostion(inputConfig.transform.position);

            if (motorControllerZone != inputZone)
            {
                errorMessage = ErrorMessages.invalidInputErrorMessage
                 + $"(Во вход для зоны '{inputZone} был подключен контроллер мотора из зоны '{motorControllerZone}')";
                return true;
            }

            errorMessage = ErrorMessages.successMessage;
            return false;
        }

        #endregion
    }
}