using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using WireVR.General;

namespace WireVR.Wires
{
    [RequireComponent(typeof(Wire))]
    public class WirePartSpawner : MonoBehaviour
    {
        #region Fields

        [Header("Wire Renderer Pooling")]
        [SerializeField]
        private WireRenderer wirePartPrefab;

        [SerializeField]
        private int wirePoolCount;

        [SerializeField]
        private bool wirePoolAutoExpand;

        private GamePool<WireRenderer> _wireRendererPool;

        private Wire _currentWire;

        private WireConfig _wireConfig;

        #endregion

        #region Properties

        public WireRenderer CurrentWireRenderer => _wireConfig.currentWireRenderer;

        #endregion

        #region Life Cycle

        private void Start()
        {
            _wireRendererPool = new GamePool<WireRenderer>(wirePartPrefab, wirePoolAutoExpand, wirePoolCount, transform);

            _wireConfig.currentWireRenderer.StartOutput = _wireConfig.startWireOutput;
        }


        private void AddListeners()
        {
            _wireConfig.endWireOutput.activated.AddListener(OnEndWireOutputActivated);
        }

        private void OnDisable()
        {
            _wireConfig.endWireOutput.activated.RemoveListener(OnEndWireOutputActivated);
        }

        #endregion

        #region Public Methods

        public void Initialize(Wire wire, WireConfig wireConfig)
        {
            _currentWire = wire;
            _wireConfig = wireConfig;

            AddListeners();

            wireConfig.currentWireRenderer.Initialize(wireConfig);
        }

        public void NewWireSpawn()
        {
            WireSpawn();
        }

        #endregion

        #region Private Methods
        
        private void OnEndWireOutputActivated(ActivateEventArgs activateEventArgs)
        {
            WireSpawn();
        }

        private void WireSpawn()
        {
            var newWire = WireCreate(_wireConfig.endWireOutput.transform);

            _wireConfig.currentWireRenderer.EndOutput = newWire.EndOutput;

            newWire.StartOutput = _wireConfig.currentWireRenderer.EndOutput;

            newWire.StartOutput.ChangeToNode();

            newWire.EndOutput = _wireConfig.endWireOutput;

            _wireConfig.endWireOutput.transform.SetParent(newWire.transform);

            _wireConfig.currentWireRenderer = newWire;

            _wireConfig.currentWireRenderer.Initialize(_wireConfig);

            _wireConfig.currentWireRenderer.NodeGeneration();
        }

        private WireRenderer WireCreate(Transform output)
        {
            var wire = _wireRendererPool.GetFreeElement();
            
            wire.transform.position = output.position;
            
            return wire;
        }

        #endregion
    }
}