using FDS.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace FDS.FlightScenarios.ScenarioActions
{
    public struct ResetComponentData
    {
        #region Fields

        public Vector3 StartPosition;
        public Quaternion Rotation;
        public Rigidbody Rigidbody;
        public IEnumerable<IResetable> Resetables;

        #endregion

        #region Constructors

        public ResetComponentData(Vector3 startPosition, Quaternion rotation, Rigidbody rigidbody, IEnumerable<IResetable> resetables)
        {
            StartPosition = startPosition;
            Rotation = rotation;
            Rigidbody = rigidbody;
            Resetables = resetables;
        }

        #endregion
    }
}
