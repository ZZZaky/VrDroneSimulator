using FDS.Quadrocopter;
using UnityEngine;

namespace FDS.UI.Views.QuadrocopterSetup
{
    class QuadrocopterParamsSetupView : View
    {
        #region Fields

        [SerializeField]
        private QuadrocopterControllerParams paramsToSetup;

        [SerializeField]
        private PidSetupView pitchSetup;

        [SerializeField]
        private PidSetupView rollSetup;

        [SerializeField]
        private PidSetupView yawSetup;

        [SerializeField]
        private FloatSetupView enginePowerDeltaLimitSetup;

        [SerializeField]
        private FloatSetupView minThrottleSetup;

        [SerializeField]
        private FloatSetupView maxThrottleSetup;

        #endregion

        #region Public Methods

        public override void InitializeView(object arg)
        {
            pitchSetup.InitializeView(paramsToSetup.PitchPID);
            rollSetup.InitializeView(paramsToSetup.RollPID);
            yawSetup.InitializeView(paramsToSetup.YawPID);
            enginePowerDeltaLimitSetup.InitializeView(paramsToSetup.EnginePowerDeltaLimit);
            enginePowerDeltaLimitSetup.OnValueApplyed += newValue => paramsToSetup.EnginePowerDeltaLimit = newValue;

            minThrottleSetup.InitializeView(paramsToSetup.MinThrottle);
            minThrottleSetup.OnValueApplyed += newValue => paramsToSetup.MinThrottle = newValue;

            maxThrottleSetup.InitializeView(paramsToSetup.MaxThrottle);
            maxThrottleSetup.OnValueApplyed += newValue => paramsToSetup.MaxThrottle = newValue;
        }
        public void SetParams(QuadrocopterControllerParams paramsObject)
        {
            paramsToSetup = paramsObject;
            InitializeView(null);
        }

        #endregion
    }
}
