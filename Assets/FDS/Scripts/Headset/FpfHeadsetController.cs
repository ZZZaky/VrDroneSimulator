using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR.Interaction.Toolkit;
using Zenject;

namespace FDS.Headset
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class FpfHeadsetController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private Camera droneCamera;

        [SerializeField]
        private XRSocketInteractor socketInteractor;

        [SerializeField]
        private RenderTexture renderTexture;

        [SerializeField]
        private GameObject fpvDroneUI;

        [SerializeField]
        private GameObject droneMonitor;

        [SerializeField]
        private DroneCameraRenderManager droneCameraRenderManager;

        [SerializeField]
        private StateMachine.StateMachine controlsStateMachine;

        private XRGrabInteractable _interactable;
        private bool _isHeadsetOn;

        #endregion

        #region Constructors

        [Inject]
        private void Constuct(PlayerChildrenObjLinker linker)
        {
            mainCamera = linker.Camera;
            socketInteractor = linker.Socket;
        }

        #endregion

        #region Events

        public event Action<bool> OnHelmetStateChanged;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _interactable = GetComponent<XRGrabInteractable>();
        }

        private void Start()
        {
            droneCamera.targetTexture = renderTexture;
            fpvDroneUI.SetActive(false);
        }

        private void OnEnable()
        {
            _interactable.selectEntered.AddListener(OnHeadsetCapture);
            _interactable.selectExited.AddListener(OnHeadsetRelease);

            socketInteractor.selectEntered.AddListener(OnHeadsetOn);
            socketInteractor.selectExited.AddListener(OnHeadsetOff);
        }

        private void OnDisable()
        {
            _interactable.selectEntered.RemoveListener(OnHeadsetCapture);
            _interactable.selectExited.RemoveListener(OnHeadsetRelease);

            socketInteractor.selectEntered.RemoveListener(OnHeadsetOn);
            socketInteractor.selectExited.RemoveListener(OnHeadsetOff);

        }

        #endregion

        #region Private Methods

        private void OnHeadsetCapture(SelectEnterEventArgs args)
        {
            if (_isHeadsetOn)
                return;
        }

        private void OnHeadsetRelease(SelectExitEventArgs args)
        {
            if (_isHeadsetOn)
                return;
        }

        private void OnHeadsetOn(SelectEnterEventArgs args)
        {
            fpvDroneUI.SetActive(true);

            droneCamera.targetTexture = null;
            _isHeadsetOn = true;

            OnHelmetStateChanged?.Invoke(false);
            
            droneCameraRenderManager.HadsetState = true;
            droneCameraRenderManager.StateCheck();

            if (controlsStateMachine.CurrentState.GetTypeOfControlState() == StateMachine.State.StateType.Character)
                ChangeMode();
        }

        private void OnHeadsetOff(SelectExitEventArgs args)
        {
            fpvDroneUI.SetActive(false);

            droneCamera.targetTexture = renderTexture;
            _isHeadsetOn = false;

            OnHelmetStateChanged?.Invoke(true);
            
            droneCameraRenderManager.HadsetState = false;
            droneCameraRenderManager.StateCheck();

            if (controlsStateMachine.CurrentState.GetTypeOfControlState() == StateMachine.State.StateType.Drone)
                ChangeMode();
        }

        private void ChangeMode()
        {
            controlsStateMachine.NextDefaultState();
            Debug.Log("ChangeMode!");
        }

        #endregion
    }
}