using ConstructionVR.Assembly.GraphSystem;
using System.Collections.Generic;
using UnityEngine;

namespace ConstructionVR.Assembly
{
    public class ConnectorsCatalog : Catalog
    {
        #region Fields

        private Dictionary<ConnectorTypeNode, List<DetailConnector>> _connectorsGroup;

        #endregion

        #region PublicMethods

        public override void Register<T>(T regrObjct)
        {
            if (regrObjct is not DetailConnector connector)
                return;

            if (!_connectorsGroup.ContainsKey(connector.ConnectorType))
                _connectorsGroup.Add(connector.ConnectorType, new());

            if (_connectorsGroup[connector.ConnectorType].Contains(connector))
            {
                Debug.LogError("ѕопытка добавить зарегистрированный конектор!");
                return;
            }

            _connectorsGroup[connector.ConnectorType].Add(connector);
        }

        public override void Unregister<T>(T unregObject)
        {
            if (unregObject is not DetailConnector connector)
                return;

            if (!_connectorsGroup.ContainsKey(connector.ConnectorType))
                return;

            if (!_connectorsGroup[connector.ConnectorType].Contains(connector))
                return;

            _connectorsGroup[connector.ConnectorType].Remove(connector);
        }
        #endregion
    }
}