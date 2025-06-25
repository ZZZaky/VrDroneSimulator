using ConstructionVR.Assembly;
using ConstructionVR.Assembly.Data;
using UnityEngine;

namespace InventorySystem
{
    public class ConnectableDetailSocket : CustomSocket
    {
        #region Fields

        [SerializeField]
        private Detail typeDetail;

        #endregion

        #region Public Methods

        public override bool CanHover(Transform itemTransform, out Item item)
        {
            return base.CanHover(itemTransform, out item)
            && (item.BaseInteractable is ConnectableDetail)
            && (item.BaseInteractable as ConnectableDetail).TypeDetail == typeDetail;
        }

        public override bool CanSelect(Item item)
        {
            return base.CanSelect(item)
            && (item.BaseInteractable is ConnectableDetail)
            && (item.BaseInteractable as ConnectableDetail).TypeDetail == typeDetail;
        }

        #endregion
    }
}
