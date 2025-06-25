using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WireVR.Wires;

namespace WireVR.Connection
{
    public class TelemInputError : InputManager
    {
        [SerializeField]
        protected WireInput input2;


        #region Public Methods

        public override bool TryGetError(out string errorMessage)
        {

            if (input.CurrentWire == null || !input.CurrentWire.IsConnected)
            {
                if (input2.CurrentWire == null || !input2.CurrentWire.IsConnected)
                {
                    errorMessage = ErrorMessages.notConnectedErrorMessage;
                    return true;
                } else { input = input2; }
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
