using ConstructionVR.Assembly.Data;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace ConstructionVR.Assembly.GraphSystem
{
    public class ConnectorTypeNode : Node
    {
        #region NestedTypes

        public enum DependetType
        {
            Dependent,
            Independent
        }

        #endregion

        #region Fields

        [Output]
        public ConnectorTypeNode dependent;

        [Output]
        public ConnectorTypeNode independent;

        [Input(connectionType = ConnectionType.Override)]
        public ConnectorTypeNode connect;

        [SerializeField]
        private int countDetail;

        [SerializeField]
        private Detail defaultConnectableDetail;

        [SerializeField]
        private List<Detail> connectableDetails = new();

        [SerializeField]
        private string pathToSaveDetail;

        #endregion

        #region Properties

        public int CountDetail { get { return countDetail; } set { countDetail = value; } }

        #endregion

        #region PublicMethods

        public void Initialize(string pathToSaveDetail)
        {
            this.pathToSaveDetail = pathToSaveDetail;
            connectableDetails = new List<Detail>();
            defaultConnectableDetail = Detail.CreateBaseDetailSO(name, pathToSaveDetail);
        }

        public DependetType GetDependentType()
        {
            foreach (var item in Inputs)
            {
                if (item.fieldName != nameof(connect))
                    continue;

                if (!item.IsConnected)
                    continue;

                return item.GetConnection(0).fieldName == nameof(independent)
                    ? DependetType.Independent : DependetType.Dependent;
            }

            return DependetType.Independent;
        }

        public Detail GetFirsConnectableDetail()
        {
            if (defaultConnectableDetail == null)
            {
                defaultConnectableDetail = Detail.CreateBaseDetailSO(name, pathToSaveDetail);
            }

            return defaultConnectableDetail;
        }

        public List<ConnectorTypeNode> GetDependentConnectors()
        {
            List<ConnectorTypeNode> result = new List<ConnectorTypeNode>();

            foreach (var item in Outputs)
            {
                if (item.fieldName != nameof(dependent))
                    continue;

                foreach (var conection in item.GetConnections())
                {
                    if (conection.node is ConnectorTypeNode nodeType)
                        result.Add(nodeType);
                }
            }

            return result;
        }

        public List<ConnectorTypeNode> GetIndependentConnectors()
        {
            List<ConnectorTypeNode> result = new List<ConnectorTypeNode>();

            foreach (var item in Outputs)
            {
                if (item.fieldName != nameof(independent))
                    continue;

                foreach (var conection in item.GetConnections())
                {
                    if (conection.node is ConnectorTypeNode nodeType)
                        result.Add(nodeType);
                }
            }

            return result;
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(connect))
            {
                return GetInputValue<ConnectorTypeNode>(nameof(connect), this.connect);
            }

            if (port.fieldName == nameof(dependent))
            {
                return GetInputValue<ConnectorTypeNode>(nameof(dependent), this.dependent);
            }

            if (port.fieldName == nameof(independent))
            {
                return GetInputValue<ConnectorTypeNode>(nameof(independent), this.independent);
            }

            return base.GetValue(port);

        }

        public List<ConnectorTypeNode> GetDependentConnected()
        {
            List<ConnectorTypeNode> result = new List<ConnectorTypeNode>();

            foreach (var item in Inputs)
            {
                if (item.fieldName != nameof(connect))
                    continue;

                foreach (var conection in item.GetConnections())
                {
                    if (conection.fieldName != nameof(dependent))
                        continue;

                    if (!(conection.node is ConnectorTypeNode nodeType))
                        continue;

                    result.Add(nodeType);
                }
            }

            return result;
        }

        public bool CheckConnectedDetail(Detail detail)
        {
            if (detail == null)
            {
                Debug.LogError("Detail!!!");
                return false;
            }

            if (connectableDetails == null)
            {
                Debug.LogError("connectableDetails!!!");
                return false;
            }

            return (connectableDetails.Contains(detail) || detail == defaultConnectableDetail);
        }

        public NodePort GetConnectedPort()
        {
            foreach (var item in Inputs)
            {
                if (item.fieldName != nameof(connect))
                    continue;

                return item;
            }

            return null;
        }

        public NodePort GetDependentPort()
        {
            foreach (var item in Outputs)
            {
                if (item.fieldName != nameof(dependent))
                    continue;

                return item;
            }

            return null;
        }

        public bool IsIndependent()
        {
            foreach (var item in Inputs)
            {
                if (item.fieldName != nameof(connect))
                    continue;

                foreach (var conection in item.GetConnections())
                {
                    if (conection.fieldName != nameof(independent))
                        return false;
                }
            }

            return true;
        }

        #endregion
    }
}