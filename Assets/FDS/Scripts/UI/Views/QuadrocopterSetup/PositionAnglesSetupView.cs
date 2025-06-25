using FDS.Drone.Inputs.ControlModes.Angles.Settings;
using UnityEngine;

namespace FDS.UI.Views.QuadrocopterSetup
{
    public class PositionAnglesSetupView : AnglesModeSetupView
    {
        #region Fields

        [SerializeField]
        private FloatSetupView distanceCoefficientSetup;

        private PositionAnglesControlModeSettings settings;

        #endregion

        #region Public Methods

        public override void InitializeView(object arg)
        {
            base.InitializeView(arg);

            settings = base.anglesSettings as PositionAnglesControlModeSettings;
            if (settings == null && base.anglesSettings != null)
                Debug.LogError($"Файл настроек типа {base.anglesSettings.GetType().Name} не допустим для данного режима. Используйте {nameof(PositionAnglesControlModeSettings)}");

            distanceCoefficientSetup.InitializeView(settings.DistanceCoefficient);
            distanceCoefficientSetup.OnValueApplyed += newValue => settings.DistanceCoefficient = newValue;
        }
        #endregion
    }
}