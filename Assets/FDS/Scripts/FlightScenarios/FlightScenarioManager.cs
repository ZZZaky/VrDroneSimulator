using UnityEngine;
using System;
using System.Collections.Generic;
using FDS.FlightScenarios.ScenarioActions;

namespace FDS.FlightScenarios
{
    class FlightScenarioManager : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private List<ScenarioAction> crashActions;

        [SerializeField]
        private Drone.Drone drone;

        #endregion


        #region Events

        public event Action CheckpointPassed;

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            drone.OnCrash += OnDroneCrashed;
        }

        private void OnDisable()
        {
            drone.OnCrash -= OnDroneCrashed;
        }

        #endregion

        #region Private Methods

        private void OnDroneCrashed()
        {
            foreach (var crashAction in crashActions)
                crashAction.ExecuteAction();
        }

        #endregion
    }
}
