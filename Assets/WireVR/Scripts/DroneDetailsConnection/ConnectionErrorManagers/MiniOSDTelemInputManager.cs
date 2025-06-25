using System.Collections.Generic;
using ConstructionVR.Assembly.Data;
using UnityEngine;

namespace WireVR.Connection
{
    public class MiniOSDTelemInputManager : InputManager
    {
        #region Fields

        [SerializeField]
        private List<Detail> forbiddenDetailsForCameraOrTBSInputs;

        #endregion

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

            if (forbiddenDetailsForCameraOrTBSInputs.Contains(inputConfig.detail))
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