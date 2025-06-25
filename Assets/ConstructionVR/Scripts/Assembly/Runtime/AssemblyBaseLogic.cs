using ConstructionVR.Assembly;
using ConstructionVR.Assembly.Data;
using ConstructionVR.Assembly.GraphSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using WireVR.Connection;
using WireVR.Wires;

public class AssemblyBaseLogic : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private AssemblyGraph assemblyGraph;

    [SerializeField]
    private DetailsController detailsController;

    [SerializeField]
    private XRInteractionManager xrInteractionManager;

    private Dictionary<ConnectorTypeNode, DetailConnectorsGroup> _detailsGroup = new();
    private DetailConnector[] _connectors;
    private int _countConnectedConectors = 0;
    private float _progress;
    private bool _isIndependent = false;

    private List<InputManager> _wireInputs;
    private int _countWireConnection = 0;

    #endregion

    #region Properties

    public float Progress
    {
        get { return _progress; }
        set
        {
            _progress = Mathf.Clamp01(value);
            OnChangeAssemblyProgress?.Invoke(_progress);
        }
    }

    #endregion

    #region Events

    public UnityEvent<float> OnChangeAssemblyProgress;
    public UnityEvent<float> OnChangeConnectionProgress;

    #endregion

    #region LifeCycle

    private void Awake()
    {
        InitDetailConnectorsGroup();
        InitWireConnection();
        detailsController.Initialize(assemblyGraph);
    }

    #endregion

    #region PublicMethods

    public void ResetAllDependent()
    {
        foreach (var detailGroup in _detailsGroup)
        {
            detailGroup.Value.SetActiveConnectorGroup(true);
        }

        _isIndependent = true;
    }

    public void SetAssemblyGraph(AssemblyGraph assemblyGraph)
    {
        this.assemblyGraph = assemblyGraph;
    }

    public void SetAllCheckAngles(bool isActive)
    {
        foreach (var connector in _connectors)
        {
            connector.SetCheckAngles(isActive);
        }
    }

    public void SetAllShowHideSteMat(bool isShow)
    {
        foreach (var connector in _connectors)
        {

            connector.ShowHideStepMaterial(isShow);
        }
    }

    [ContextMenu("Auto build")]
    public void AutoBuild()
    {
        StartCoroutine(AutoBuildWaitt());
    }

    IEnumerator AutoBuildWaitt()
    {
        foreach (var node in assemblyGraph.nodes)
        {
            foreach (var connector in _detailsGroup[node as ConnectorTypeNode].Connectors)
            {
                var connectableDetail = detailsController
                    .GetDisconnectedDetail(connector.ConnectorType.GetFirsConnectableDetail());

                if (connectableDetail != null)
                {
                    connector.IsAutoBuild = true;
                    connector.SetActiveConnector(true);
                    xrInteractionManager.SelectEnter(connector, connectableDetail as IXRSelectInteractable);
                    Debug.Log(connectableDetail.name);

                }
                else
                {
                    Debug.Log("!!!!!!" + connector.ConnectorType);
                    continue;
                }

                yield return new WaitForSeconds(1);
            }
        }

    }

    #endregion

    #region PrivateMethods

    private void InitWireConnection()
    {
        var wires = FindObjectsOfType<InputManager>();
        _wireInputs = new List<InputManager>(wires.Length);

        foreach (var inputManager in wires)
        {
            if (inputManager != null)
            {
                inputManager.WireInput.onChangeCurrentWireConnected += WireInput_onChangeCurrentWireConnected;
                _wireInputs.Add(inputManager);
            }
        }

        Debug.Log("Wires count = " + _wireInputs.Count);
    }

    private void WireInput_onChangeCurrentWireConnected(WireInput arg1, Wire arg2, ConstructionVR.Assembly.Data.Detail arg3)
    {
        _countWireConnection += arg2 != null ? +1 : -1;
        _countWireConnection = Mathf.Clamp(_countWireConnection, 0, _wireInputs.Count);
        var prog = Mathf.Clamp01((float)_countWireConnection / (float)_wireInputs.Count);

        OnChangeConnectionProgress?.Invoke(prog);
    }

    private void InitDetailConnectorsGroup()
    {
        var allConnectors = FindObjectsOfType<DetailConnector>();
        List<DetailConnector> connectors = new List<DetailConnector>();

        foreach (var connector in allConnectors)
        {
            if (assemblyGraph.nodes.Contains(connector.ConnectorType))
            {
                connectors.Add(connector);
            }
        }

        _connectors = connectors.ToArray();

        for (int i = 0; i < _connectors.Length; i++)
        {
            if (!assemblyGraph.nodes.Contains(_connectors[i].ConnectorType))
            {
                Debug.LogError($"Connector type {_connectors[i].ConnectorType.name} not found in assembly graph!!!");
                continue;
            }

            if (_detailsGroup.ContainsKey(_connectors[i].ConnectorType))
            {
                _detailsGroup[_connectors[i].ConnectorType].AddDetail(_connectors[i]);
            }
            else
            {
                var detailGroup = new DetailConnectorsGroup(OnChangeDetailGroupStatys, _connectors[i].ConnectorType);
                detailGroup.AddDetail(_connectors[i]);
                _detailsGroup.Add(_connectors[i].ConnectorType, detailGroup);
            }

            _connectors[i].OnChangeStatys.AddListener(OnChangeConnectorStatys);
        }

        //По умолчанию запрещаем установку всех деталей
        foreach (var detailGroup in _detailsGroup)
        {
            detailGroup.Value.SetActiveConnectorGroup(false);
        }

        //Разрешаем установку независимых деталей
        foreach (var node in assemblyGraph.nodes)
        {
            if ((node as ConnectorTypeNode).IsIndependent())
            {
                if (_detailsGroup.ContainsKey(node as ConnectorTypeNode))
                {
                    _detailsGroup[node as ConnectorTypeNode].SetActiveConnectorGroup(true);
                }
            }
        }

        //Активируем установку первой детали
        _detailsGroup[(assemblyGraph.nodes[0] as ConnectorTypeNode)].SetActiveConnectorGroup(true);
        _detailsGroup[(assemblyGraph.nodes[0] as ConnectorTypeNode)].SetActiveStepMaterial(true);
    }

    private void OnChangeConnectorStatys(ConnectionStatys arg0, DetailConnector arg1)
    {
        _countConnectedConectors += arg0 == ConnectionStatys.Connected ? 1 : -1;
        _countConnectedConectors = Mathf.Clamp(_countConnectedConectors, 0, _connectors.Length);

        Progress = _countConnectedConectors / (float)_connectors.Length;
    }

    private void OnChangeDetailGroupStatys(DetailConnectorsGroup.GroupStatys statys, ConnectorTypeNode connectorType)
    {
        if (_isIndependent)
            return;

        //Разрешаем или запрещаем установку деталей, которые зависят от текущей детали
        foreach (var connector in connectorType.GetDependentConnectors())
        {
            if (_detailsGroup.ContainsKey(connector))
            {
                _detailsGroup[connector].SetActiveConnectorGroup(statys == DetailConnectorsGroup.GroupStatys.Ready);
                _detailsGroup[connector].SetActiveStepMaterial(statys == DetailConnectorsGroup.GroupStatys.Ready);
            }
        }

        //Запрещаем или разрешаем снятие детали от которой зависят другие детали
        foreach (var connected in connectorType.GetDependentConnected())
        {
            if (_detailsGroup.ContainsKey(connected))
            {
                _detailsGroup[connected].SetActiveConnectorGroup(statys == DetailConnectorsGroup.GroupStatys.Empty);
                _detailsGroup[connected].SetActiveStepMaterial(statys == DetailConnectorsGroup.GroupStatys.Empty);
            }
        }

        //Подсвечиваем независимые детали идущие за этим шагом
        foreach (var connected in connectorType.GetIndependentConnectors())
        {
            if (_detailsGroup.ContainsKey(connected))
            {
                _detailsGroup[connected].SetActiveStepMaterial(true);
            }
        }
    }

    #endregion
}
