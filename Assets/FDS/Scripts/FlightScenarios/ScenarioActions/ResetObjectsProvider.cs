using System.Collections.Generic;
using UnityEngine;

namespace FDS.FlightScenarios.ScenarioActions
{
    public class ResetObjectsProvider : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private List<Transform> components;

        #endregion

        #region Public Methods

        public IEnumerable<Transform> GetResetComponents()
        {
            return components;
        }

        #endregion
    }
}
