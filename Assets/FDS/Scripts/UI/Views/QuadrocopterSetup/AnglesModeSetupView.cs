using FDS.Drone.Inputs.ControlModes.Angles.Settings;
using FDS.UI.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FDS.UI.Views.QuadrocopterSetup
{
    public class AnglesModeSetupView : View
    {
        #region Fields

        [SerializeField]
        protected AnglesControlModeSettings anglesSettings;

        [SerializeField]
        private FloatSetupView yawSpeedSetup;

        [SerializeField]
        private FloatSetupView velocityCoefficientSetup;

        #endregion

        #region Public Methods

        public override void InitializeView(object arg)
        {
            yawSpeedSetup.InitializeView(anglesSettings.YawChangingSpeed);
            velocityCoefficientSetup.InitializeView(anglesSettings.VelocityCoefficient);

            yawSpeedSetup.OnValueApplyed += newValue => anglesSettings.YawChangingSpeed = newValue;
            velocityCoefficientSetup.OnValueApplyed += newValue => anglesSettings.VelocityCoefficient = newValue;
        }

        #endregion
    }
}