using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FDS.Drone.Components
{
    public class DronePropellerRotator : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private float inertia;

        #endregion

        #region Properties

        public float Inertia => inertia;
        public float RotationSpeed { get; private set; }

        #endregion

        #region LifeCycle

        private void FixedUpdate()
        {
            var currentAngle = RotationSpeed * 360;
            transform.Rotate(0, currentAngle, 0);
        }

        #endregion

        #region Public Methods

        public void SetRotation(float speed)
        {
            RotationSpeed = speed;
        }

        #endregion
    }
}