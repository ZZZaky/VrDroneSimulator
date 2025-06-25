using ConstructionVR.Assembly;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace WireVR.General
{
    [RequireComponent(typeof(XRBaseControllerInteractor))]
    public class HandObjectTracker : MonoBehaviour
    {
        #region Fields

        private XRBaseControllerInteractor _interactor;

        private XRGrabInteractable _lastCapturedObject;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            _interactor = GetComponent<XRBaseControllerInteractor>();
        }

        private void OnEnable()
        {
            _interactor.selectEntered.AddListener(OnSelectEntered);
            _interactor.hoverEntered.AddListener(OnHoverEnter);
            _interactor.hoverExited.AddListener(OnHoverExited);
        }


        private void OnDisable()
        {
            _interactor.selectEntered.RemoveListener(OnSelectEntered);
            _interactor.hoverEntered.RemoveListener(OnHoverEnter);
            _interactor.hoverExited.RemoveListener(OnHoverExited);
        }

        #endregion

        #region Public Methods

        public bool IsLastCapturedObject(XRGrabInteractable grabInteractable)
        {
            return _lastCapturedObject == grabInteractable;
        }

        #endregion

        #region Private Methods

        private void OnHoverEnter(HoverEnterEventArgs arg0)
        {
            if (arg0.interactableObject is ConnectableDetail detail)
            {
                if (detail.isSelected)
                    return;

                detail.Outline.enabled = true;
            }
        }

        private void OnHoverExited(HoverExitEventArgs arg0)
        {
            if (arg0.interactableObject is ConnectableDetail detail)
            {
                detail.Outline.enabled = false;
            }
        }

        private void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
        {
            if (selectEnterEventArgs.interactableObject is XRGrabInteractable)
                _lastCapturedObject = (XRGrabInteractable)selectEnterEventArgs.interactableObject;

            if (selectEnterEventArgs.interactableObject is ConnectableDetail detail)
            {
                detail.Outline.enabled = false;
            }
        }

        #endregion
    }
}
