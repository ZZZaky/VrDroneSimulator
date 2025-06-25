using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace InventorySystem
{
    public class Item : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private XRBaseInteractable baseInteractable;

        private Rigidbody _rigidbody;

        #endregion

        #region Properties

        public bool IsSelected { get; set; }

        public XRBaseInteractable BaseInteractable => baseInteractable;

        public Rigidbody Rigidbody => _rigidbody;

        public bool InitialRigidbodyUseGravity { get; private set; }

        public bool InitialRigidbodyIsKinematic { get; private set; }

        public bool IsInteractable => baseInteractable.transform == transform;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            if (baseInteractable == null)
                if (!TryGetBaseInteractable(transform, out baseInteractable))
                    Debug.LogError($"[Item]: There must be an {typeof(XRBaseInteractable)} component on the object or on child objects");

            _rigidbody = baseInteractable.GetComponent<Rigidbody>();

            InitialRigidbodyUseGravity = _rigidbody.useGravity;
            InitialRigidbodyIsKinematic = _rigidbody.isKinematic;

            if (!IsInteractable && baseInteractable.colliders.Count > 0)
            {
                if (!baseInteractable.colliders[0].TryGetComponent<ItemChild>(out var itemChild))
                    itemChild = baseInteractable.colliders[0].gameObject.AddComponent<ItemChild>();
                itemChild.ItemParent = this;
            }
        }

        #endregion

        private bool TryGetBaseInteractable(Transform currentTransform, out XRBaseInteractable interactable)
        {
            if (currentTransform.TryGetComponent<XRBaseInteractable>(out interactable))
                return true;
            else
            {
                foreach (Transform child in currentTransform)
                    if (TryGetBaseInteractable(child, out interactable))
                        return true;

                return false;
            }

        }
    }
}
