using FDS.Drone.Components;
using UnityEngine;

namespace FDS.UI.Views.QuadrocopterSetup
{
    class PidSetupView : TypeSetupView<PidController>
    {
        #region Fields

        [SerializeField]
        private FloatSetupView pSetup;

        [SerializeField]
        private FloatSetupView iSetup;

        [SerializeField]
        private FloatSetupView dSetup;

        private PidController pid;

        #endregion

        #region Public Methods

        public override void InitializeView(object arg)
        {
            base.InitializeView(arg);
            pid = (PidController)arg;

            pSetup.InitializeView(pid.P);
            iSetup.InitializeView(pid.I);
            dSetup.InitializeView(pid.D);

            pSetup.OnValueApplyed += newValue => pid.P = newValue;
            iSetup.OnValueApplyed += newValue => pid.I = newValue;
            dSetup.OnValueApplyed += newValue => pid.D = newValue;
        }

        #endregion
    }
}
