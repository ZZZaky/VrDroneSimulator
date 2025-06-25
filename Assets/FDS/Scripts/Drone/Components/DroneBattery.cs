using FDS.Interfaces;
using System;
using TMPro;
using Tools.Timer;
using UnityEngine;

namespace FDS.Drone.Components
{
    public class DroneBattery : MonoBehaviour, IResetable
    {
        #region Fields

        [SerializeField]
        private float workingTime;

        [SerializeField]
        private Vector2 batteryVoltageValueRange;


        [SerializeField] private float criticalBatteryLevel;


        private float aCoeff;
        private float declineCoeff = .45f;
        private Timer _timer;
        private bool _isBatteryLow = false;

        #endregion

        #region Properties

        public float Voltage { get; private set; }
        public float Percent { get; private set; }

        #endregion

        #region Events

        public event Action<bool> OnBatteryLevelChanged;

        #endregion

        #region LifeCycle

        private void Start()
        {
            _timer = new Timer();
            aCoeff = (batteryVoltageValueRange.y - batteryVoltageValueRange.x) / Mathf.Pow(workingTime, declineCoeff);
            Percent = 100f;
            _timer.StartTimer();
        }

        private void Update()
        {
            var time = _timer.GetTime();
            var seconds = time.Hours * 60 * 60 + time.Minutes * 60 + time.Seconds;

            Percent = Mathf.Max(100 - Mathf.Ceil(seconds / workingTime * 100), 0);
            Voltage = VoltageFunction(seconds);

            if(!_isBatteryLow && Percent < criticalBatteryLevel)
            {
                _isBatteryLow = true;
                OnBatteryLevelChanged?.Invoke(_isBatteryLow);
            }

        }

        #endregion

        #region Protected Methods

        protected virtual float VoltageFunction(int time)
        {
            if (time >= workingTime)
                return batteryVoltageValueRange.x;

            return aCoeff * Mathf.Pow(-time + workingTime, declineCoeff) + batteryVoltageValueRange.x;
        }

        void IResetable.Reset()
        {
            _timer.StartTimer();
            Percent = 100f;
            _isBatteryLow = false;
            OnBatteryLevelChanged?.Invoke(_isBatteryLow);
        }

        #endregion
    }
}