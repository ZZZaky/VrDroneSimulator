using System.Collections.Generic;
using ConstructionVR.Assembly;
using ConstructionVR.Assembly.Data;
using UnityEngine;
using WireVR.Wires;

namespace WireVR.Connection
{
    public class ConnectableObject : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        protected List<WireInput> inputs;

        [SerializeField]
        protected ConnectableDetail connectableDetail;

        #endregion

        #region Life Cycle

        private void OnEnable()
        {
            foreach (var input in inputs)
                input.onChangeCurrentWireConnected += OnChangeConnected;
        }

        private void OnDisable()
        {
            foreach (var input in inputs)
                input.onChangeCurrentWireConnected -= OnChangeConnected;
        }

        #endregion

        #region Protected Methods

        protected virtual void OnChangeConnected(WireInput wireInput, Wire wire, Detail detail)
        {
            if (wire != null && wire.Data == null)
                wire.Data = connectableDetail.TypeDetail;
        }

        #endregion
    }
}