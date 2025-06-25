using FDS.Drone.ControlModes;
using FDS.Drone.Inputs.ControlModes.Angles.Settings;
using UnityEngine;

namespace FDS.Drone.Inputs.ControlModes.Angles
{
    public class PositionRetentionAnglesControlMode : ForwardRightVelocityResetControlMode
    {
        #region Fields

        private PositionAnglesControlModeSettings settings;
        private Vector3 _targetPosition;

        private bool _freeFlying;

        #endregion

        #region LifeCycle

        private void Start()
        {
            settings = base.controlModeSettings as PositionAnglesControlModeSettings;
            if (settings == null)
                Debug.LogError($"Файл настроек типа {base.controlModeSettings?.GetType().Name} не допустим для данного режима. Используйте {nameof(PositionAnglesControlModeSettings)}");
        }

        #endregion

        #region Public Methods

        public override DroneAngles GetTargetAngles(ControlModeInputData inputData)
        {
            if (inputData.ForwardDelta == 0 && inputData.RightDelta == 0)
            {
                if(_freeFlying)
                {
                    _freeFlying = false;
                    OnSelect();
                }
                var positionDelta = droneBodyRb.transform.InverseTransformDirection(_targetPosition - drone.GetCurrentPosition());
                inputData.ForwardDelta = positionDelta.x;
                inputData.RightDelta = positionDelta.z;
            }
            else
            {
                _freeFlying = true;
            }

            return base.GetTargetAngles(inputData);
        }

        public override float GetRightShift(float rightDelta)
        {
            if(_freeFlying)
                return -rightDelta * settings.RightSpeed;
            else
                return rightDelta * settings.DistanceCoefficient;
        }

        public override float GetForwardShift(float forwardDelta)
        {
            if (_freeFlying)
                return forwardDelta * settings.ForwardSpeed;
            else
                return forwardDelta * settings.DistanceCoefficient;
        }

        public override void OnSelect()
        {
            _targetPosition = drone.GetCurrentPosition();
        }

        #endregion
    }
}
