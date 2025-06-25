using ConstructionVR.Assembly.GraphSystem;
using UnityEngine;

namespace ConstructionVR.Assembly.Components
{
    public class DetailMarker : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private bool isParentPart = false;

        [SerializeField]
        private ConnectorTypeNode connectorType;

        #endregion

        #region Properties

        public bool IsParentPart => isParentPart;

        public ConnectorTypeNode ConnectorType => connectorType;

        #endregion

        #region PublicMethods

        public void SetConnectorNode(ConnectorTypeNode connectorType)
        {
            this.connectorType = connectorType;
        }

        #endregion
    }
}