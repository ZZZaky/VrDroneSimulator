using FDS.Interfaces;
using System;
using UnityEngine;

namespace FDS.Drone.Components
{
    [Serializable]
    public class PidController : IResetable
    {
        #region Fields

        [SerializeField]
        private float p;

        [SerializeField]
        private float i;

        [SerializeField]
        private float d;

        private float _prevError;
        private float _sumError;

        #endregion

        #region Properties

        public float P 
        {
            get => p;
            set
            {
                p = value;
            }
        }

        public float I
        {
            get => i;
            set
            {
                i = value;
            }
        }

        public float D
        {
            get => d;
            set
            {
                d = value;
            }
        }

        #endregion

        #region Contructors

        public PidController(float p, float i, float d)
        {
            this.p = p;
            this.i = i;
            this.d = d;
        }

        #endregion

        #region Public Methods

        public float Calculate(float current, float target)
        {
            var dt = Time.fixedDeltaTime;

            var err = target - current;
            _sumError += err;

            var force = p * err + i * _sumError * dt + d * (err - _prevError) / dt;

            _prevError = err;
            return force;
        }

        public void Reset()
        {
            _prevError = 0;
            _sumError = 0;
        }

        #endregion

    }
}
