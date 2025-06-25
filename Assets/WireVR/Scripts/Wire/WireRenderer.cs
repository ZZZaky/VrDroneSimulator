using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using WireVR.General;

namespace WireVR.Wires
{
    public class WireRenderer : MonoBehaviour
    {
        #region Fields

        [Header("Line Rendering")]
        [SerializeField]
        private LineRenderer lineRenderer;


        [SerializeField]
        private int initialCount;

        [Header("Node Pooling")]
        [SerializeField]
        private WireNode nodePrefab;

        [SerializeField]
        private int nodePoolCount;

        [SerializeField]
        private bool nodePoolAutoExpand;

        [Header("Wire Outputs")]
        [SerializeField]
        private WireOutput endOutput;

        ///////
        [SerializeField]
        private WireOutput _startOutput;

        private List<Transform> _nodes = new List<Transform>();

        private GamePool<WireNode> _nodePool;

        private WireConfig _wireConfig; 

        #endregion

        #region Properties

        public WireOutput StartOutput
        {
            get => _startOutput;

            set 
            {
                RemoveListeners();
                
                _startOutput = value;

                AddListeners();
            }
        }

        public WireOutput EndOutput
        {
            get => endOutput;

            set
            {
                RemoveListeners();

                endOutput = value;
                
                AddListeners();

                SetEndWireOutputConnectedBodies();            
            }
        }

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _nodePool = new GamePool<WireNode>(nodePrefab, nodePoolAutoExpand, nodePoolCount, transform);
        }

        private void Start()
        {
            lineRenderer.startWidth = _wireConfig.lineWight;
            lineRenderer.endWidth = _wireConfig.lineWight;

            NodeGeneration(initialCount);
        }

        private void Update() 
        {
            LineRendering();
            #if UNITY_EDITOR
            lineRenderer.startWidth = _wireConfig.lineWight;
            lineRenderer.endWidth = _wireConfig.lineWight;
            #endif
        }

        #endregion

        #region Public Methods

        public void Initialize(WireConfig wireConfig)
        {
            _wireConfig = wireConfig;
                
            lineRenderer.colorGradient = wireConfig.wireGradient;
        }

        public void NodeGeneration(int initialCount = 2)
        {
            var count = (int)(Vector3.Distance(StartOutput.transform.position, EndOutput.transform.position) / _wireConfig.density);
            if (count <= 0)
                count = initialCount;

            if (count > _nodes.Count)
            {
                for (int i = _nodes.Count; i <= count; i++) 
                {
                    var spawnedNode = Spawn();

                    if (i != 0)
                        spawnedNode.transform.localPosition = _nodes[i-1].transform.localPosition;
                }
            }
            else if (count < _nodes.Count)
            {
                for (int i = count; i < _nodes.Count; i++)
                    Despawn(_nodes[i]);

                _nodes.RemoveAll(node => node.gameObject.activeSelf == false);
            }

            if (_nodes.Count > 0)
                SetNodesConnectedBody();
        }

        public void SetEndWireOutputConnectedBodies()
        {
            if (_nodes.Count > 0)
                EndOutput.GetComponent<HingeJoint>().connectedBody = _nodes[_nodes.Count-1].GetComponent<Rigidbody>();
            else
                EndOutput.GetComponent<HingeJoint>().connectedBody = StartOutput.GetComponent<Rigidbody>();
        }

        #endregion

        #region Private Methods

        private void LineRendering()
        {
            lineRenderer.positionCount = _nodes.Count + 2;

            lineRenderer.SetPosition(0,StartOutput.transform.localPosition);

                for (int i = 0; i < _nodes.Count; i++)
                    lineRenderer.SetPosition(i+1,_nodes[i].localPosition);
            
            if (EndOutput.transform.parent == transform)
                lineRenderer.SetPosition(lineRenderer.positionCount - 1,EndOutput.transform.localPosition);
        }

        private void AddListeners()
        {
            EndOutput?.selectExited.AddListener(OnWireOutputSelectExited);
            StartOutput?.selectExited.AddListener(OnWireOutputSelectExited);
        }
        
        private void RemoveListeners()
        {
            EndOutput?.selectExited.RemoveListener(OnWireOutputSelectExited);
            StartOutput?.selectExited.RemoveListener(OnWireOutputSelectExited);
        }

        private void OnWireOutputSelectExited(SelectExitEventArgs selectExitEventArgs)
        {
            if (selectExitEventArgs.interactorObject is XRRayInteractor || selectExitEventArgs.interactorObject is XRDirectInteractor)
                NodeGeneration();
        }

        private WireNode Spawn()
        {
            var spawnedNode = _nodePool.GetFreeElement();
            
            _nodes.Add(spawnedNode.transform);

            return spawnedNode;
        }

        private void Despawn(Transform objectForDespawn)
        {
            objectForDespawn.gameObject.SetActive(false);
        }

        private void SetNodesConnectedBody()
        {
            _nodes[0].GetComponent<HingeJoint>().connectedBody = StartOutput.GetComponent<Rigidbody>();
            EndOutput.GetComponent<HingeJoint>().connectedBody = _nodes[_nodes.Count-1].GetComponent<Rigidbody>();

            for (int i = 1; i < _nodes.Count; i++)
                _nodes[i].GetComponent<HingeJoint>().connectedBody = _nodes[i-1].GetComponent<Rigidbody>();
        }

        #endregion
    }
}