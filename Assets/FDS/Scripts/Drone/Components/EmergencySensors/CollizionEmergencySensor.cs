using FDS.Interfaces;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace FDS.Drone.Components.EmergencySensors
{
    [RequireComponent(typeof(Collider))]
    public class CollizionEmergencySensor : EmergencySensor, IResetable
    {
        #region Fields

        [SerializeField]
        private LayerMask ignoreLayer;

        private Stack<Collider> _collizions = new Stack<Collider>();

        #endregion

        #region LifeCycle

        private void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsLayerInMask(ignoreLayer, other.gameObject.layer))
                return;
            _collizions.Push(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (LayerUtils.IsLayerInMask(ignoreLayer, other.gameObject.layer))
                return;
            _collizions.Pop();
        }

        #endregion

        #region Public Methods

        public override bool IsEmergencySituation(Drone drone)
        {
            return _collizions.Count != 0;
        }

        #endregion

        #region Private Methods

        void IResetable.Reset()
        {
            _collizions.Clear();
        }

        #endregion
    }
}
