using UnityEngine;

namespace Tools
{
    public static class LayerUtils
    {
        #region Public Methods

        public static bool IsLayerInMask(LayerMask mask, int layer)
        {
            return (mask & (1 << layer)) != 0;
        }

        #endregion
    }
}
