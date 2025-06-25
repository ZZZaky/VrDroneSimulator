using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace WireVR.Wires
{
    [RequireComponent(typeof(WireOutput))]
    public class ManyWireConnector : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private List<Wire> wires;

        private WireOutput _wireOutput;

        #endregion

        #region Properties

        public bool IsConnected => _wireOutput.isSelected && _wireOutput.firstInteractorSelecting is WireInput;

        public List<Wire> Wires => wires;

        #endregion

        #region Events

        public event Action<List<Wire>> onChangeWiresConnected;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            _wireOutput = GetComponent<WireOutput>();
        }

        private void Start()
        {
            foreach (var wire in wires)
            {
                var fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = wire.WireConfig.endWireOutput.GetComponent<Rigidbody>();
            }
        }

        private void OnEnable()
        {
            _wireOutput.selectEntered.AddListener(OnSelectEntered);
            _wireOutput.activated.AddListener(OnActivated);
        }

        private void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
        {
            foreach (var wire in wires)
            {
                wire.IsConnected = IsConnected && wire.WireConfig.startWireOutput.IsConnected;
            }
        }

        private void OnDisble()
        {
            _wireOutput.selectEntered.RemoveListener(OnSelectEntered);
            _wireOutput.activated.RemoveListener(OnActivated);
        }


        #endregion

        #region Private Methods

        private void OnActivated(ActivateEventArgs arg0)
        {
            for (int i = 0; i < wires.Count; i++)
            {
                wires[i].WirePartSpawner.NewWireSpawn();

                GetComponents<FixedJoint>()[i].connectedBody = wires[i].WireConfig.endWireOutput.GetComponent<Rigidbody>();
            }
        }

        private void OnSelectExited(SelectExitEventArgs selectExitEventArgs)
        {
            foreach (var wire in wires)
                wire.WirePartSpawner.CurrentWireRenderer.NodeGeneration();
        }

        #endregion
    }
}
