using FDS.Drone.Components;
using FDS.Drone.Components.EmergencySensors;
using FDS.Drone.Inputs;
using FDS.Environment;
using FDS.FlightScenarios.ScenarioActions;
using FDS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FDS.Drone
{
    public abstract class Drone : MonoBehaviour, IResetable
    {
        #region Fields

        [SerializeField]
        private bool droneEnabled;

        [SerializeField]
        private Transform bodyTransform;

        [SerializeField]
        private Rigidbody bodyRb;

        [SerializeField] private DroneBattery battery;

        [SerializeField]
        private EnvironmentParamsProvider envParamsProvider;

        [SerializeField]
        private List<EmergencySensor> emergencySensors;

        #endregion

        #region Properties

        public Rigidbody BodyRb => bodyRb;
        public Transform BodyTransform => bodyTransform;
        public GameObject Body => bodyTransform.gameObject;
        public bool IsDroneCrashed { get; private set; }
        public EnvironmentParamsProvider EnvironmentParamsProvider => envParamsProvider;

        public bool IsDroneEnabled => droneEnabled;
        public Vector3 Home { get; private set; }

        #endregion

        #region Events

        public event Action OnCrash;
        public event Action OnDroneEnabled;
        public event Action OnDroneDisabled;

        #endregion

        #region LifeCycle

        private void Start()
        {
            Home = bodyTransform.position;
            if (droneEnabled)
                EnableDrone();
        }

        private void FixedUpdate()
        {
            if (IsDroneCrashed || !IsDroneEnabled)
                return;

            var angles = ReadCurrentAngles();
            if (IsSituationEmergency() || battery.Percent <= 0)
            {
                IsDroneCrashed = true;
                StopEngines();
                OnCrash?.Invoke();
            }
            else
            {
                Stabilize();
            }
        }

        #endregion

        #region Public Methods

        public void EnableDrone()
        {
            droneEnabled = true;
            OnDroneEnabled?.Invoke();
        }

        public void DisableDrone()
        {
            droneEnabled = false;
            OnDroneDisabled?.Invoke();
        }

        public bool IsSituationEmergency()
        {
            return emergencySensors.Count != 0 && emergencySensors.Any(e => e.IsEmergencySituation(this));
        }

        public DroneAngles ReadCurrentAngles()
        {
            return new DroneAngles(bodyTransform.rotation.eulerAngles.z, bodyTransform.rotation.eulerAngles.y, bodyTransform.rotation.eulerAngles.x);
        }

        public Vector3 GetCurrentPosition()
        {
            return bodyTransform.position;
        }

        #endregion

        #region Protected Methods

        protected abstract void Stabilize();

        protected abstract void StopEngines();

        #endregion

        #region Private Methods

        void IResetable.Reset()
        {
            IsDroneCrashed = false;
        }

        #endregion
    }
}
