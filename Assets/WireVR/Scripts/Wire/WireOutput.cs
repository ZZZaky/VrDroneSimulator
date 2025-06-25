using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace WireVR.Wires
{
    public class WireOutput : XRGrabInteractable
    {   
        #region Fields

        [Header("Output Config")]
        [SerializeField]
        private bool isInitialConnected;

        [SerializeField]
        private GameObject  wireOutputModel;

        [SerializeField]
        private Mesh wireOutputMesh;

        private Transform _parent;

        #endregion

        #region Properties

        public bool IsConnected => isSelected && firstInteractorSelecting is WireInput;
        public bool CanConnect => WireOutputCaptureChecker.Instance.IsGrabbed(this) || isInitialConnected;

        public Mesh WireOutputMesh => wireOutputMesh;

        public Wire CurrentWire { get; set; }

        #endregion

        #region Life Cycle

        protected override void Awake()
        {
            base.Awake();
            _parent = transform.parent;
        }

        #endregion

        #region Public Methods

        public void ChangeToNode()
        {
            if (wireOutputModel == null)
                wireOutputModel = transform.GetChild(0).gameObject;
                
            wireOutputModel.SetActive(false);
        }

        #endregion

        #region Protected Methods

        protected override void OnSelectEntering(SelectEnterEventArgs selectEnterEventArgs)
        {
            base.OnSelectEntering(selectEnterEventArgs);
            
            if (selectEnterEventArgs.interactorObject is XRDirectInteractor)
                (selectEnterEventArgs.interactorObject as XRDirectInteractor).xrController.model.gameObject.SetActive(false);
        }
        
        protected override void OnSelectExiting(SelectExitEventArgs selectExitEventArgs)
        {
            base.OnSelectExiting(selectExitEventArgs);

            if (selectExitEventArgs.interactorObject is XRDirectInteractor)
                (selectExitEventArgs.interactorObject as XRDirectInteractor).xrController.model.gameObject.SetActive(true);
        }

        protected override void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
        {
            base.OnSelectEntered(selectEnterEventArgs);

            transform.SetParent(_parent);
        }

        #endregion
    }
}