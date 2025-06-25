using FDS.Drone.ControlModes;
using UnityEngine;

namespace FDS.Drone.Inputs.ControlModes.Throttle
{
    public class AltitudeRetentionThrottleControlMode : VerticalSpeedResetControlMode
    {
        #region Fields

        private AltitudeThrottleControlModeSettings settings;

        private float _targetY;
        private bool _freeFlying;

        #endregion

        #region LifeCycle

        private void Start()
        {
            settings = base.controlModeSettings as AltitudeThrottleControlModeSettings;
            if (settings == null && base.controlModeSettings != null)
                Debug.LogError($"Файл настроек типа {base.controlModeSettings?.GetType().Name} не допустим для данного режима. Используйте {nameof(AltitudeThrottleControlModeSettings)}");
        }

        #endregion

        #region Public Methods

        public override float GetThrottle(ControlModeInputData inputData)
        {
            if (inputData.UpDelta >= 0.5 - settings.EnableSpeedResetInterval / 2
                && inputData.UpDelta <= 0.5 + settings.EnableSpeedResetInterval)
            {
                if (_freeFlying)
                {
                    OnSelect();
                    _freeFlying = false;
                }
                return base.GetThrottle(inputData);
            }
            _freeFlying = true;
            return settings.ThrottleChangingSpeed * inputData.UpDelta;
        }
        public override float GetShiftY(float upAxis)
        {
            var droneY = drone.GetCurrentPosition().y;
            var deltaY = _targetY - droneY;
            return settings.DistanceCoefficient * deltaY;
        }

        public override void OnSelect()
        {
            _targetY = drone.GetCurrentPosition().y;
        }

        #endregion
    }
}
