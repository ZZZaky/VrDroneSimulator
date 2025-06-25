namespace WireVR.Connection
{
    public class MiniOSDCameraOrTBSInputManager : InputManager
    {
        #region Public Methods

        public override bool TryGetError(out string errorMessage)
        {
            if (input.CurrentWire  == null || !input.CurrentWire.IsConnected)
            {
                errorMessage = ErrorMessages.notConnectedErrorMessage;
                return true;
            }

            if (!(input.CurrentWire.Data is MiniOSDInputConfig))
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