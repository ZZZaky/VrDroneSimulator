using ConstructionVR.Assembly.GraphSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ConstructionVR.Assembly
{
    public class DetailConnectorsGroup
    {
        #region NestedTypes

        public enum GroupStatys
        {
            Ready,
            AssemblyProcess,
            Empty
        }

        #endregion

        #region Fields

        private Action<GroupStatys, ConnectorTypeNode> _onChangeStatys;

        private List<DetailConnector> _allConnectors = new();

        private int _countConnected = 0;

        private bool _isSingleType;

        #endregion

        #region Properties

        public ConnectorTypeNode ConnectorType { get; private set; }

        public List<DetailConnector> Connectors => _allConnectors;

        #endregion

        #region Construction

        public DetailConnectorsGroup(Action<GroupStatys, ConnectorTypeNode> onChangeStatys, ConnectorTypeNode typeNode, bool isSingleType = true)
        {
            _onChangeStatys = onChangeStatys;
            ConnectorType = typeNode;
            _isSingleType = isSingleType;
        }

        #endregion

        #region PublicMethods

        public void SetActiveConnectorGroup(bool isActive)
        {
            foreach (var connector in _allConnectors)
            {
                connector.SetActiveConnector(isActive);
            }
        }

        public void SetActiveStepMaterial(bool isActive)
        {
            foreach (var connector in _allConnectors)
            {
                connector.SetActiveStepMaterial(isActive);
            }
        }

        public void AddDetail(DetailConnector connector)
        {
            if (_isSingleType)
                if (connector.ConnectorType != ConnectorType)
                {
                    Debug.LogError("Попытка добавить конектор не принадлежащий типу группы!");
                    return;
                }

            _allConnectors.Add(connector);
            connector.OnChangeStatys.AddListener(ConnectorOnChangeStatys);
        }

        #endregion

        #region PrivateMethods

        private void ConnectorOnChangeStatys(ConnectionStatys statys, DetailConnector detailConnector)
        {
            _countConnected += statys == ConnectionStatys.Connected ? 1 : -1;

            if (_allConnectors.Count == _countConnected)
            {
                _onChangeStatys?.Invoke(GroupStatys.Ready, ConnectorType);
            }
            else if (_countConnected > 0)
            {
                _onChangeStatys?.Invoke(GroupStatys.AssemblyProcess, ConnectorType);
            }
            else
            {
                _onChangeStatys?.Invoke(GroupStatys.Empty, ConnectorType);
            }

            _countConnected = Mathf.Clamp(_countConnected, 0, _allConnectors.Count);
        }

        #endregion
    }
}
