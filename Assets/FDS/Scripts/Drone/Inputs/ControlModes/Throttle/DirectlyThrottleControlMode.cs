using FDS.Drone.ControlModes;
using UnityEngine;

namespace FDS.Drone.Inputs.ControlModes.Throttle
{
    public class DirectlyThrottleControlMode : VerticalSpeedResetControlMode
    {
        #region Fields

        private DirectThrottleControlModeSettings settings;

        #endregion

        #region LifeCycle

        private void Start()
        {
            settings = base.controlModeSettings as DirectThrottleControlModeSettings;
            if (settings == null)
                Debug.LogError($"Файл настроек типа {base.controlModeSettings.GetType().Name} не допустим для данного режима. Используйте {nameof(DirectThrottleControlModeSettings)}");
        }

        #endregion

        #region Public Methods

        public override float GetThrottle(ControlModeInputData inputData)
        {
            if (inputData.UpDelta >= 0.5 - settings.EnableSpeedResetInterval / 2
                && inputData.UpDelta <= 0.5 + settings.EnableSpeedResetInterval)
                return base.GetThrottle(inputData);
            return settings.ThrottleChangingSpeed * inputData.UpDelta;
        }

        #endregion
    }
}
