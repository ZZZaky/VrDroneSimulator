using FDS.Environment;
using UnityEngine;

namespace FDS.UI.Views.EnvironmentSetup
{
    public class EnvironmentSetupView : View
    {
        #region Fields

        [SerializeField]
        private EnvironmentSettings settings;

        [SerializeField]
        private FloatSetupView thrustCoefficient;

        [SerializeField]
        private FloatSetupView globalWindMaxPower;

        [SerializeField]
        private FloatSetupView globalWindFrequency;

        #endregion

        #region Public Methods

        public override void InitializeView(object arg)
        {
            base.InitializeView(arg);

            thrustCoefficient.InitializeView(settings.AerodynamicThrustCoefficient);
            thrustCoefficient.OnValueApplyed += e => settings.AerodynamicThrustCoefficient = e;

            globalWindMaxPower.InitializeView(settings.MaxGlobalWindPower);
            globalWindMaxPower.OnValueApplyed += e => settings.MaxGlobalWindPower = e;

            globalWindFrequency.InitializeView(settings.GlobalWindOscilationFrequency);
            globalWindFrequency.OnValueApplyed += e => settings.GlobalWindOscilationFrequency = e;
        }

        #endregion
    }
}