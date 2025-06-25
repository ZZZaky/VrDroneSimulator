using FDS.Drone.Inputs.ControlModes.Throttle;
using UnityEngine;

namespace FDS.UI.Views.QuadrocopterSetup
{
    public class DirectThrottleSetupView : ThrottleModeSetupView
    {
        #region Fields

        [SerializeField]
        private FloatSetupView throttleChangingSpeedSetup;

        [SerializeField]
        private FloatSetupView enableSpeedResetIntervalSetup;

        private DirectThrottleControlModeSettings settings;

        #endregion

        #region Public Methods

        public override void InitializeView(object arg)
        {
            base.InitializeView(arg);

            settings = base.throttleSettings as DirectThrottleControlModeSettings;
            if (settings == null && base.throttleSettings != null)
                Debug.LogError($"Файл настроек типа {base.throttleSettings.GetType().Name} не допустим для данного режима. Используйте {nameof(DirectThrottleControlModeSettings)}");
        
            throttleChangingSpeedSetup.InitializeView(settings.ThrottleChangingSpeed);
            enableSpeedResetIntervalSetup.InitializeView(settings.EnableSpeedResetInterval);

            throttleChangingSpeedSetup.OnValueApplyed += newValue => settings.ThrottleChangingSpeed = newValue;
            enableSpeedResetIntervalSetup.OnValueApplyed += newValue => settings.EnableSpeedResetInterval = newValue;
        }

        #endregion
    }
}