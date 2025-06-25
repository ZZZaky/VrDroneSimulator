using UnityEngine;
using WireVR.Wires;

namespace InventorySystem
{
    public class WireCustomSocket : CustomSocket
    {
        #region Public Methods

        public override bool CanHover(Transform itemTransform, out Item item)
        {
            return base.CanHover(itemTransform, out item) 
            && (item.BaseInteractable is WireOutput);
        }

        public override bool CanSelect(Item item)
        {
            return base.CanSelect(item)
            && (item.BaseInteractable is WireOutput);
        }

        #endregion
    }
}
