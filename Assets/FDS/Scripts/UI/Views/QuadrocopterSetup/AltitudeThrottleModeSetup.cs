using FDS.Drone.Inputs.ControlModes.Throttle;
using UnityEngine;

namespace FDS.UI.Views.QuadrocopterSetup
{
    public class AltitudeThrottleModeSetup : ThrottleModeSetupView
    {
        #region Fields

        [SerializeField]
        private FloatSetupView distanceCoefficientSetup;

        private AltitudeThrottleControlModeSettings settings;

        #endregion

        #region Public Methods

        public override void InitializeView(object arg)
        {
            base.InitializeView(arg);

            settings = base.throttleSettings as AltitudeThrottleControlModeSettings;
            if (settings == null && base.throttleSettings != null)
                Debug.LogError($"Файл настроек типа {base.throttleSettings.GetType().Name} не допустим для данного режима. Используйте {nameof(AltitudeThrottleControlModeSettings)}");

            distanceCoefficientSetup.InitializeView(settings.DistanceCoefficient);

            distanceCoefficientSetup.OnValueApplyed += newValue => settings.DistanceCoefficient = newValue;

        }

        #endregion
    }
}