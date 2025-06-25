using System;
using ConstructionVR.Assembly.Data;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace WireVR.Wires
{
    public class WireInput : XRSocketInteractor
    {   
        #region Fields

        [SerializeField]
        private Mesh allowConnectorMesh;

        [SerializeField]
        private Detail allowDetail;

        private Wire _currentWire;

        private object _data;

        #endregion

        #region Properties

        public Wire CurrentWire
        {
            get => _currentWire;

            set 
            {
                if (value != null)
                {
                    _currentWire = value;
                    _currentWire.onChangeConnected += OnChangeWireConnected;
                    _currentWire.onChangeData += OnChangeWireData;
                }
                else
                {
                    if (_currentWire != null)
                    {
                        _currentWire.onChangeConnected -= OnChangeWireConnected;
                        _currentWire.onChangeData -= OnChangeWireData;
                    }

                    _currentWire = null;

                    OnChangeWireConnected(null);
                }
            }
        }

        public Detail AllowDetail => allowDetail;

        #endregion

        #region Events

        public event Action<WireInput ,Wire, Detail> onChangeCurrentWireConnected;
        public event Action<WireInput, Wire,object> onChangeCurrentWireData;

        #endregion

        #region Life Cycle

        protected override void Awake()
        {
            base.Awake();
            
            interactableCantHoverMeshMaterial = null;
        }

        #endregion

        #region Public Methods

        public override bool CanHover(IXRHoverInteractable interactable)
        {
            return base.CanHover(interactable)
                && interactable is WireOutput
                && (interactable as WireOutput).WireOutputMesh == allowConnectorMesh
                && (interactable as WireOutput).CanConnect;
        }

        public override bool CanSelect(IXRSelectInteractable interactable)
        {
            if (interactable is WireOutput && (interactable as WireOutput).IsConnected)
                return base.CanSelect(interactable);

            return base.CanSelect(interactable)
                && interactable is WireOutput
                && (interactable as WireOutput)?.WireOutputMesh == allowConnectorMesh
                && (interactable as WireOutput).CanConnect;
        }

        #endregion

        #region Protected Methods

        protected override void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
        {
            if (!(selectEnterEventArgs.interactableObject as WireOutput).CanConnect) return;

            base.OnSelectEntered(selectEnterEventArgs);

            if (selectEnterEventArgs.interactableObject.transform.TryGetComponent<ManyWireConnector>(out var manyWireConnector))
            {
                CurrentWire = manyWireConnector.Wires[0];
                return;
            }
            if (selectEnterEventArgs.interactableObject is WireOutput)
            {
                var output = selectEnterEventArgs.interactableObject as WireOutput;
                
                CurrentWire = output.CurrentWire;
            }
        }

        protected override void OnSelectExited(SelectExitEventArgs selectExitEventArgs)
        {
            base.OnSelectExited(selectExitEventArgs);

            CurrentWire = null;
        }

        #endregion

        #region Private Methods

        private void OnChangeWireConnected(Wire currentWire)
        {
            onChangeCurrentWireConnected?.Invoke(this, CurrentWire, allowDetail);
        }

        private void OnChangeWireData(object data)
        {
            onChangeCurrentWireData?.Invoke(this,CurrentWire,data);
        }

        #endregion
    }
}