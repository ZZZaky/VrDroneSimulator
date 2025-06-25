using UnityEngine;
using UnityEngine.Rendering;

namespace FDS.Effects
{
    public class PostProcessingSignalEffect : SignalEffect
    {
        #region Fields

        [SerializeField]
        private Volume volume;

        #endregion

        #region Public Methods

        public override void UpdateEffect(float signalPower)
        {
            volume.weight = 1 - signalPower;
        }

        #endregion
    }
}
