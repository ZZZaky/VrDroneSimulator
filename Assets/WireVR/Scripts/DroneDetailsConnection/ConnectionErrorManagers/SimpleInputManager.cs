namespace WireVR.Connection
{
    public class SimpleInputManager : InputManager
    {
        #region Public Methods

        public override bool TryGetError(out string errorMessage)
        {
            if (input.CurrentWire  == null || !input.CurrentWire.IsConnected)
            {
                errorMessage = ErrorMessages.notConnectedErrorMessage;
                return true;
            }

            errorMessage = ErrorMessages.successMessage;
            return false;
        }

        #endregion
    }
}