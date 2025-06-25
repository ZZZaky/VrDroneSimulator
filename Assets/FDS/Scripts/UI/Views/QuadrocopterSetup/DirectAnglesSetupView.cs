using FDS.Drone.Inputs.ControlModes.Angles.Settings;
using UnityEngine;

namespace FDS.UI.Views.QuadrocopterSetup
{
    public class DirectAnglesSetupView : AnglesModeSetupView
    {
        #region Fields

        [SerializeField]
        private FloatSetupView forwardSpeedSetup;

        [SerializeField]
        private FloatSetupView rightSpeedSetup;

        private DirectAnglesControlModeSettings settings;

        #endregion

        #region Public Methods

        public override void InitializeView(object arg)
        {
            base.InitializeView(arg);

            settings = base.anglesSettings as DirectAnglesControlModeSettings;
            if (settings == null && base.anglesSettings != null)
                Debug.LogError($"Файл настроек типа {base.anglesSettings.GetType().Name} не допустим для данного режима. Используйте {nameof(DirectAnglesControlModeSettings)}");

            forwardSpeedSetup.InitializeView(settings.ForwardSpeed);
            rightSpeedSetup.InitializeView(settings.RightSpeed);

            forwardSpeedSetup.OnValueApplyed += newValue => settings.ForwardSpeed = newValue;
            rightSpeedSetup.OnValueApplyed += newValue => settings.RightSpeed = newValue;
        }
        #endregion
    }
}