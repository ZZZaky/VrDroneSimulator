using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tools.Timer
{
    public class Timer
    {
        #region Fields

        private float _startTime;
        private float _beginingTime;
        private List<TimerTime> _iterations = new List<TimerTime>();

        #endregion

        #region Properties

        public TimerTime ElapsedTime { get; private set; }
        public bool IsRunning { get; private set; }
        public List<TimerTime> Iterations => _iterations;

        #endregion

        #region Events

        public event Action OnTimerStarted;
        public event Action<TimerTime> OnTimerStop;
        public event Action OnCircle;

        #endregion

        #region Public Methods

        public void StartTimer()
        {
            _startTime = Time.time;
            _beginingTime = Time.time;
            IsRunning = true;
            OnTimerStarted?.Invoke();
            _iterations.Clear();
        }

        public void NewIteration(bool resetTime = false)
        {
            _iterations.Add(GetTime());
            if (resetTime)
                _startTime = Time.time;

            OnCircle?.Invoke();
        }

        public void StopTimer()
        {
            ElapsedTime = GetTime(true);
            IsRunning = false;
            OnTimerStop?.Invoke(ElapsedTime);
        }

        public void ResetTimer()
        {
            _iterations.Clear();
            IsRunning = false;
        }

        public TimerTime GetTime(bool getTotalTime = false)
        {
            if (!IsRunning)
                return ElapsedTime;

            var time = getTotalTime ? _beginingTime : _startTime;

            var ellapsedMs = (int)Mathf.Floor((Time.time - time) * 1000);
            var ellapsedSeconds = (int)Mathf.Floor(Time.time - time);
            var ellapsedMinutes = (int)Mathf.Floor(ellapsedSeconds / 60);
            var ellapsedHours = (int)Mathf.Floor(ellapsedMinutes / 60);

            return new TimerTime(ellapsedHours, ellapsedMinutes - ellapsedHours * 60, ellapsedSeconds - ellapsedMinutes * 60, ellapsedMs - ellapsedSeconds * 1000);
        }

        public float GetSeconds()
        {
            if (!IsRunning)
                return ElapsedTime.ToSeconds();
            return (int)Mathf.Floor(Time.time - _startTime);
        }

        public TimerTime GetBestTime()
        {
            TimerTime bestTime = _iterations[0];

            foreach (var timerTime in _iterations)
            {
                if (bestTime > timerTime)
                {
                    bestTime = timerTime;
                }
            }

            return bestTime;
        }

        #endregion
    }

}
