using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace WireVR.Wires
{
    [RequireComponent(typeof(WirePartSpawner))]
    public class Wire : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private WireConfig wireConfig;

        private WirePartSpawner _wirePartSpawner;

        private bool _isConnected;

        private object _data;

        [SerializeField]
        private bool show;

        #endregion

        #region Properties

        public bool IsConnected
        {
            get => _isConnected;

            set
            {
                _isConnected = value;

                onChangeConnected?.Invoke(this);
            }
        }

        public object Data
        {
            get => _data;

            set
            {
                _data = value;

                onChangeData?.Invoke(value);
            }
        }

        public WirePartSpawner WirePartSpawner
        {
            get
            {
                if (_wirePartSpawner == null)
                    _wirePartSpawner = GetComponent<WirePartSpawner>();
                return _wirePartSpawner;
            }
        }

        public WireConfig WireConfig => wireConfig;

        #endregion

        #region Events

        public event Action<Wire> onChangeConnected;

        public event Action<object> onChangeData;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            WirePartSpawner.Initialize(this, wireConfig);
            wireConfig.startWireOutput.CurrentWire = this;
            wireConfig.endWireOutput.CurrentWire = this;
        }

        private void OnEnable()
        {
            wireConfig.startWireOutput.selectEntered.AddListener(OnSelectEntered);
            wireConfig.endWireOutput.selectEntered.AddListener(OnSelectEntered);
            wireConfig.startWireOutput.selectExited.AddListener(OnSelectExited);
            wireConfig.endWireOutput.selectExited.AddListener(OnSelectExited);
        }

        private void OnDisable()
        {
            wireConfig.startWireOutput.selectEntered.RemoveListener(OnSelectEntered);
            wireConfig.endWireOutput.selectEntered.RemoveListener(OnSelectEntered);
            wireConfig.startWireOutput.selectExited.RemoveListener(OnSelectExited);
            wireConfig.endWireOutput.selectExited.RemoveListener(OnSelectExited);
        }

        private void Update()
        {
            if (show)
            {
                show = false;
                Debug.Log(Data);
            }
        }

        #endregion

        #region Private Methods

        private void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
        {
            if (selectEnterEventArgs.interactorObject is WireInput)
                IsConnected = wireConfig.startWireOutput.IsConnected && wireConfig.endWireOutput.IsConnected;
        }

        private void OnSelectExited(SelectExitEventArgs selectExitEventArgs)
        {
            if (selectExitEventArgs.interactorObject is WireInput)
                IsConnected = false;
        }

        #endregion
    }
}