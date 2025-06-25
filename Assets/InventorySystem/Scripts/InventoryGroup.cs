using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ConstructionVR.Assembly.Data;
using ConstructionVR.Assembly;

namespace InventorySystem
{
    [Serializable]
    public struct InventoryItemConfig
    {
        public Item item;

        public int count;
    }

    public class InventoryGroup : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private bool isSetUpInStart;

        [SerializeField]
        private List<InventoryItemConfig> _items;

        [SerializeField]
        private int maxCountItemsInSlot;

        [SerializeField]
        private float itemScaleCoef;

        private List<CustomSocket> _sockets;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            _sockets = GetComponentsInChildren<CustomSocket>().ToList();

            if (!isSetUpInStart) return;

            for (int i = 0; i < _sockets.Count; i++)
            {
                if (_items.Count > i)
                    _sockets[i].SetUp(_items[i], maxCountItemsInSlot, itemScaleCoef);
                else
                    _sockets[i].SetUp(new InventoryItemConfig(), maxCountItemsInSlot, itemScaleCoef);
            }
        }

        #endregion

        #region Public Methods

        public bool TryGetItemsByConnectableDetailType(Detail detailType, out Item[] items)
        {
            List<Item> foundItems = new List<Item>();

            foreach (var socket in _sockets)
            {
                if (socket.CurrentItem == null) continue;

                if (socket.CurrentItem.BaseInteractable is ConnectableDetail)
                    if ((socket.CurrentItem.BaseInteractable as ConnectableDetail).TypeDetail == detailType)
                        foundItems.Add(socket.CurrentItem);
            }

            if (foundItems.Count <= 0)
            {
                items = null;
                return false;
            }

            items = foundItems.ToArray();
            return true;
        }

        #endregion
    }
}
