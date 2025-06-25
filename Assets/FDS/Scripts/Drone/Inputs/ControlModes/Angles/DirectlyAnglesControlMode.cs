using UnityEngine;
using FDS.Drone.Inputs.ControlModes.Angles.Settings;

namespace FDS.Drone.Inputs.ControlModes.Angles
{
    public class DirectlyAnglesControlMode : ForwardRightVelocityResetControlMode
    {
        #region Fields

        private DirectAnglesControlModeSettings settings;

        #endregion

        #region LifeCycle

        private void Start()
        {
            settings = base.controlModeSettings as DirectAnglesControlModeSettings;
            if (settings == null)
                Debug.LogError($"Файл настроек типа {base.controlModeSettings?.GetType().Name} не допустим для данного режима. Используйте {nameof(DirectAnglesControlModeSettings)}");
        }


        #endregion

        #region Public Methods

        public override float GetForwardShift(float forwardDelta)
        {
            return forwardDelta * settings.ForwardSpeed;
        }

        public override float GetRightShift(float rightDelta)
        {
            return -rightDelta * settings.RightSpeed;
        }

        public override void OnSelect()
        {

        }

        #endregion
    }
}
