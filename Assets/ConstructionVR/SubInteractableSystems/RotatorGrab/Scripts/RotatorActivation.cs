using ConstructionVR.Assembly;
using ConstructionVR.Assembly.GraphSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ConstructionVR.SubInteractableSystem.RotatorGrab
{
    public class RotatorActivation : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private List<DetailConnector> connectors;

        #endregion

        #region Events

        public UnityEvent<bool> OnChangeActive;

        #endregion

        #region LifeCycle

        private void Start()
        {
            var connectorGroup = new DetailConnectorsGroup(OnChangeGroupStatus, null, false);

            foreach (var connector in connectors)
            {
                connectorGroup.AddDetail(connector);
            }

            OnChangeActive?.Invoke(false);
        }

        #endregion

        #region PrivateMethods

        private void OnChangeGroupStatus(DetailConnectorsGroup.GroupStatys status, ConnectorTypeNode arg2)
        {
            OnChangeActive?.Invoke(status == DetailConnectorsGroup.GroupStatys.Ready);
        }

        #endregion
    }
}