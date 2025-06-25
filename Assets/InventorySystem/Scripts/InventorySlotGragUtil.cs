using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace InventorySystem
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class InventorySlotGragUtil : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private CustomSocket socket;

        private XRGrabInteractable _grabInteractable;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();

            if (socket == null)
                socket = transform.parent.GetComponentInChildren<CustomSocket>();
        }

        private void OnEnable()
        {
            _grabInteractable.selectEntered.AddListener(OnGrab);
            _grabInteractable.selectExited.AddListener(OnSelectExit);
        }


        private void OnDisable()
        {
            _grabInteractable.selectEntered.RemoveListener(OnGrab);
            _grabInteractable.selectExited.RemoveListener(OnSelectExit);
        }

        #endregion

        #region Private Methods

        private void OnGrab(SelectEnterEventArgs selectEnterEventArgs)
        {
            _grabInteractable.interactionManager.SelectExit(selectEnterEventArgs.interactorObject, _grabInteractable);

            if (socket.CurrentItem != null)
                _grabInteractable.interactionManager.SelectEnter(selectEnterEventArgs.interactorObject, socket.CurrentItem.BaseInteractable);
        }

        private void OnSelectExit(SelectExitEventArgs selectExitEventArgs)
        {
            _grabInteractable.transform.localPosition = Vector3.zero;
        }

        #endregion
    }
}
