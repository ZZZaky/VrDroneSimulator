using UnityEngine;

namespace FDS.Effects
{
    public abstract class SignalEffect : MonoBehaviour
    {
        #region Public Methods

        public abstract void UpdateEffect(float signalPower);

        #endregion
    }
}
