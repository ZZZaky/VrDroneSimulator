namespace WireVR.Connection
{
    public class PixhawkTelemInputManager : InputManager
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

            var inputConfig = (MiniOSDInputConfig)input.CurrentWire.Data;

            if (input.AllowDetail != inputConfig.detail)
            {
                errorMessage = ErrorMessages.invalidDetailErrorMessage;
                return true;
            }

            if (input.GetComponent<MinimOSDInputsEnum>().MinimOSDInputType != inputConfig.minimOSDInputType)
            {
                errorMessage = ErrorMessages.invalidInputErrorMessage;
                return true;
            }

            errorMessage = ErrorMessages.successMessage;
            return false;
        }

        #endregion
    }
}