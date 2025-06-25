using FDS.Drone.ControlModes;
using FDS.Drone.Inputs.ControlModes.Angles;
using FDS.Drone.Inputs.ControlModes.Throttle;
using FDS.Interfaces;
using UnityEngine;

namespace FDS.Drone.Inputs
{
    public class DroneManagement : MonoBehaviour, IResetable
    {
        #region Fields

        [SerializeField]
        private Drone drone;

        [SerializeField]
        private DroneInputs droneInputs;

        [SerializeField]
        private DronePropellerAnimator Propeller;

        [SerializeField]
        private DronePropellerAnimator Propeller1;

        [SerializeField]
        private DronePropellerAnimator Propeller2;

        [SerializeField]
        private DronePropellerAnimator Propeller3;

        private AnglesControlMode _anglesControlMode;
        private ThrottleControlMode _throttleControlMode;

        private DroneAngles _currentAngles;
        private DroneAngles _targetAngles;
        private DroneAngles _startAngles;
        private float _throttle;

        #endregion

        #region Properties

        public DroneAngles TargetAngles => _targetAngles;
        public DroneAngles CurrentAngles => _currentAngles;
        public float Throttle => _throttle;

        #endregion

        #region LifeCycle

        private void Start()
        {
            _currentAngles = drone.ReadCurrentAngles();
            _startAngles = _currentAngles;
            _targetAngles = _currentAngles;
        }

        private void Update()
        {
            _currentAngles = drone.ReadCurrentAngles();

            var forwardDelta = droneInputs.GetForwardAxisConverted();
            var rightDelta = droneInputs.GetRightAxisConverted();
            var rotationDelta = droneInputs.GetRotationAxisConverted();
            var upDelta = droneInputs.GetUpAxisConverted();

            var controlModeData = new ControlModeInputData(forwardDelta, rightDelta, rotationDelta, upDelta, TargetAngles, Throttle);

            _throttle = _throttleControlMode.GetThrottle(controlModeData);
            _targetAngles = _anglesControlMode.GetTargetAngles(controlModeData);

            DronePropellerAnimationCheck(_throttle);
        }

        #endregion

        #region Public Methods

        public void ChangeThrottleControlMode(ThrottleControlMode newControlMode)
        {
            newControlMode.OnSelect();
            _throttleControlMode = newControlMode;
        }

        public void ChangeAngleControlMode(AnglesControlMode newControlMode)
        {
            newControlMode.OnSelect();
            _anglesControlMode = newControlMode;
        }

        #endregion

        #region Private Methods

        void IResetable.Reset()
        {
            _targetAngles = _startAngles;
        }

        private void DronePropellerAnimationCheck(float throttle)
        {
            if (throttle == 0)
            {
                Propeller.DisableRotation();
                Propeller1.DisableRotation();
                Propeller2.DisableRotation();
                Propeller3.DisableRotation();
            }
            else
            {
                Propeller.EnableRotation();
                Propeller1.EnableRotation();
                Propeller2.EnableRotation();
                Propeller3.EnableRotation();
            }
        }
    }

        #endregion
 }
