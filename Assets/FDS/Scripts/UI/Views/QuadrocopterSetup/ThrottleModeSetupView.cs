using FDS.Drone.Inputs.ControlModes.Throttle;
using UnityEngine;

namespace FDS.UI.Views.QuadrocopterSetup
{
    public class ThrottleModeSetupView : View
    {
        #region Fields

        [SerializeField]
        protected ThrottleControlModeSettings throttleSettings;

        [SerializeField]
        private FloatSetupView velocityCoefficientSetup;

        #endregion

        #region Public Methods

        public override void InitializeView(object arg)
        {
            velocityCoefficientSetup.InitializeView(throttleSettings.VelocityCoefficient);
            velocityCoefficientSetup.OnValueApplyed += newValue => throttleSettings.VelocityCoefficient = newValue;
        }

        #endregion
    }
}