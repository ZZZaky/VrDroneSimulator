using ConstructionVR.Assembly.Data;

namespace WireVR.Connection
{
    public class PixhawkSimpleInputManager : InputManager
    {
        #region Public Methods

        public override bool TryGetError(out string errorMessage)
        {
            if (input.CurrentWire  == null || !input.CurrentWire.IsConnected)
            {
                errorMessage = ErrorMessages.notConnectedErrorMessage;
                return true;
            }

            if (!(input.CurrentWire.Data is Detail))
            {
                errorMessage = ErrorMessages.invalidDetailErrorMessage;
                return true;
            }

            var detail = (Detail)input.CurrentWire.Data;

            if (input.AllowDetail != detail)
            {
                errorMessage = ErrorMessages.invalidDetailErrorMessage;
                return true;
            }

            errorMessage = ErrorMessages.successMessage;
            return false;
        }

        #endregion
    }
}
