using FDS.Environment;
using UnityEngine;

namespace FDS.Drone.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class DroneEngine : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Drone drone;

        [SerializeField]
        private bool rotatePropellers;

        [SerializeField]
        private Rigidbody droneBody;

        [SerializeField]
        private DronePropellerRotator propeller;

        [SerializeField]
        private bool rotateClockwise;

        private Rigidbody _rb;

        #endregion

        #region Properties

        public float Power { get; private set; }

        public float AngularVelocity { get; private set; }

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rb.AddRelativeForce(0, Power, 0);
        }

        #endregion

        #region Public Methods

        public void SetAngleFrequency(float frequency)
        {
            AngularVelocity = frequency;
            Power = Mathf.Pow(frequency, 2) * drone.EnvironmentParamsProvider.Params.AerodynamicThrustCoefficient;
            RotatePropellers();
        }

        public void SetPower(float power)
        {
            Power = power;
            AngularVelocity = Mathf.Sqrt(Mathf.Abs(Power) / drone.EnvironmentParamsProvider.Params.AerodynamicThrustCoefficient);
            RotatePropellers();
        }

        #endregion

        #region Private Methods

        private void RotatePropellers()
        {
            if (rotatePropellers)
                propeller.SetRotation((rotateClockwise ? 1 : -1) * AngularVelocity);

            var propellerInertiaImpulse = propeller.Inertia * Mathf.Pow(AngularVelocity, 2);
            droneBody.AddTorque((rotateClockwise ? -1 : 1) * propellerInertiaImpulse * Vector3.up);
        }

        #endregion
    }
}
