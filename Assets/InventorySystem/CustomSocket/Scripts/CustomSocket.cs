using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace InventorySystem
{
    public class CustomSocket : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Transform attachTransform;

        [SerializeField]
        private Item startingSelectedItemPrefab;

        [SerializeField]
        private int startingSelectedItemCount = 1;

        [SerializeField]
        private int maxCount = 1;

        [SerializeField]
        private bool isResizingItem;

        [SerializeField]
        private float itemScaleCoef = 1;

        [Header("LOGS")]
        [SerializeField]
        private bool isLogging;

        private List<Item> _items = new List<Item>();

        private Item _currentItem;

        private float _socketObjectSize;

        private Vector3 _initialItemScale;

        private Transform _initialItemParent;

        private bool _isSelected;

        private bool _currentItemInitialUseGravity;

        private bool _currentItemInitialIsKinematic;

        #endregion

        #region Properties

        public int Count => _items.Count;

        public int MaxCount => maxCount;

        public Item CurrentItem => _currentItem;

        #endregion

        #region Events

        public event Action OnHoverEntered;

        public event Action OnHoverExited;

        public event Action OnSelectEntered;

        public event Action OnSelectExited;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            if (startingSelectedItemCount > maxCount)
                startingSelectedItemCount = maxCount;
        }

        private void Start()
        {
            if (startingSelectedItemPrefab == null) return;

            for (int i = 0; i < startingSelectedItemCount; i++)
            {
                var startingItem = startingSelectedItemPrefab;

                if (string.IsNullOrWhiteSpace(startingItem.gameObject.scene.name))
                    startingItem = Instantiate(startingSelectedItemPrefab);

                HoverEnter(startingItem);
                SelectEnter(startingItem);
            }
        }

        private void OnTriggerEnter(Collider itemCollider)
        {
            if (CanHover(itemCollider.transform, out var item))
            {
                if (isLogging)
                    Debug.Log("Trigger Enter " + itemCollider.name);
                HoverEnter(item);
            }
        }

        private void OnTriggerExit(Collider itemCollider)
        {
            if (CanHover(itemCollider.transform, out var item))
            {
                if (isLogging)
                    Debug.Log("Trigger Exit " + itemCollider.name);
                HoverExit(item);
            }
        }

        #endregion

        #region Public Methods

        public void SetUp(InventoryItemConfig itemConfig, int maxCount, float scaleCoef)
        {
            this.startingSelectedItemPrefab = itemConfig.item;
            this.startingSelectedItemCount = itemConfig.count;
            this.maxCount = maxCount;
            this.itemScaleCoef = scaleCoef;
        }

        public virtual bool CanHover(Transform itemTransform, out Item item)
        {
            return TryGetItem(itemTransform, out item) && !item.IsSelected;
        }

        public virtual bool CanSelect(Item item)
        {
            return _items.Count < maxCount && !item.IsSelected && !item.BaseInteractable.isSelected;
        }

        #endregion
        
        #region Protected Methods

        protected virtual void HoverEnter(Item item)
        {
            if (_items.Count >= maxCount) return;

            item.BaseInteractable.selectExited.AddListener(ItemInteractableSelectExited);
            item.BaseInteractable.selectEntered.AddListener(ItemInteractableSelectEntered);

            if (!_items.Contains(item))
                OnHoverEntered?.Invoke();

            if (isLogging)
                Debug.Log("Hover Enter " + item.name);
        }

        protected virtual void HoverExit(Item item)
        {
            item.BaseInteractable.selectExited.RemoveListener(ItemInteractableSelectExited);
            item.BaseInteractable.selectEntered.RemoveListener(ItemInteractableSelectEntered);

            OnHoverExited?.Invoke();

            if (isLogging)
                Debug.Log("Hover Exit " + item.name);
        }

        protected virtual void SelectEnter(Item item)
        {
            if (!CanSelect(item)) return;

            item.IsSelected = true;

            if (_currentItem != null)
                item.gameObject.SetActive(false);

            item.Rigidbody.useGravity = false;
            item.Rigidbody.isKinematic = true;

            _initialItemParent = item.transform.parent;

            if (_initialItemParent == transform)
                _initialItemParent = null;

            item.transform.SetParent(transform);

            if (attachTransform == null)
                item.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
            else
                item.transform.SetLocalPositionAndRotation(attachTransform.localPosition, attachTransform.localRotation);

            if (!item.IsInteractable)
                item.BaseInteractable.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));

            if (!_isSelected)
            {
                _initialItemScale = item.transform.lossyScale;

                if (isResizingItem)
                    Resize(item);

                _currentItem = item;
                _currentItemInitialUseGravity = _currentItem.InitialRigidbodyUseGravity;
                _currentItemInitialIsKinematic = _currentItem.InitialRigidbodyIsKinematic;

                _isSelected = true;
            }

            if (!_items.Contains(item))
                _items.Add(item);

            OnSelectEntered?.Invoke();
            OnHoverExited?.Invoke();

            if (isLogging)
                Debug.Log("Select Enter " + item.name);
        }

        protected virtual void SelectExit(Item item)
        {
            item.BaseInteractable.selectExited.AddListener(ItemBaseInteractableSelectExit);

            item.IsSelected = false;

            if (_items.Contains(item))
                _items.Remove(item);

            _currentItem = null;
            _isSelected = false;

            if (_items.Count > 0)
            {
                _items[0].gameObject.SetActive(true);
                SelectEnter(_items[0]);
            }

            OnSelectExited?.Invoke();
            HoverExit(item);

            if (isLogging)
                Debug.Log("Select Exit " + item.name);
        }


        #endregion

        #region Private Methods

        private bool TryGetItem(Transform objectToTry, out Item item)
        {
            if (!objectToTry.TryGetComponent<Item>(out item))
                if (objectToTry.TryGetComponent<ItemChild>(out var itemChild))
                {
                    item = itemChild.ItemParent;
                    return true;
                }
                else return false;

            return true;
        }

        private void ItemInteractableSelectExited(SelectExitEventArgs selectExitEventArgs)
        {
            if (TryGetItem(selectExitEventArgs.interactableObject.colliders[0]?.transform, out var item))
                SelectEnter(item);
        }

        private void ItemInteractableSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
        {
            if (TryGetItem(selectEnterEventArgs.interactableObject.colliders[0]?.transform, out var item))
                SelectExit(item);
        }

        private void ItemBaseInteractableSelectExit(SelectExitEventArgs selectExitEventArgs)
        {
            if (!TryGetItem(selectExitEventArgs.interactableObject.colliders[0]?.transform, out var item)) return;

            item.transform.SetParent(_initialItemParent);

            item.Rigidbody.useGravity = _currentItemInitialUseGravity;

            item.Rigidbody.isKinematic = _currentItemInitialIsKinematic;

            if (isResizingItem)
                item.transform.localScale = _initialItemScale;

            selectExitEventArgs.interactableObject.selectExited.RemoveListener(ItemBaseInteractableSelectExit);
        }

        private void Resize(Item item)
        {
            var itemBoundsSize = CalculateNestedObjectBounds(item.transform).size;

            var itemObjectSize = Mathf.Max(itemBoundsSize.x, itemBoundsSize.y, itemBoundsSize.z);

            var scaleFactor = (CalculateSocketSize() / itemObjectSize);

            item.transform.localScale *= scaleFactor;
        }

        private Bounds CalculateNestedObjectBounds(Transform rootObject)
        {
            var childRenderers = rootObject.GetComponentsInChildren<MeshRenderer>();

            var objectBounds = new Bounds(rootObject.position, Vector3.zero);

            foreach (var childRenderer in childRenderers)
            {
                if (childRenderer.enabled)
                    objectBounds.Encapsulate(childRenderer.bounds);
            }

            foreach (Transform childTransform in rootObject)
            {
                if (!(childTransform.TryGetComponent<MeshRenderer>(out var meshRenderer) && meshRenderer.enabled)) break;

                var childBounds = CalculateNestedObjectBounds(childTransform);
                objectBounds.Encapsulate(childBounds);
            }

            return objectBounds;
        }

        private float CalculateSocketSize()
        {
            if (_socketObjectSize > 0)
                return _socketObjectSize;

            var slotBoundsSize = transform.GetComponent<Collider>().bounds.size * itemScaleCoef;
            _socketObjectSize = Mathf.Max(slotBoundsSize.x, slotBoundsSize.y, slotBoundsSize.z);

            return _socketObjectSize;
        }

        #endregion
    }
}
