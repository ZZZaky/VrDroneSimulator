using UnityEngine.UI;

namespace FDS.UI
{
    public class NormalizedSlider : Slider
    {
        #region Properties

        public float NormalizedValue => base.value / base.maxValue;

        #endregion
    }
}
