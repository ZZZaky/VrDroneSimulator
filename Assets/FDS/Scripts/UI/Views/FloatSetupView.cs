using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FDS.UI.Views
{
    class FloatSetupView : TypeSetupView<float>
    {
        #region Fields

        [SerializeField]
        private TextMeshProUGUI sliderValueLabel;

        [SerializeField]
        [Range(2,5)]
        private int floatLength = 2;

        [SerializeField]
        private Slider valueSlider;

        [SerializeField]
        private float maxValue;

        [SerializeField]
        private float minValue;

        #endregion

        #region Public Methods

        public override void InitializeView(object arg)
        {
            base.InitializeView(arg);

            valueSlider.maxValue = maxValue;
            valueSlider.minValue = minValue;
            valueSlider.onValueChanged.AddListener(SliderValueChanged);

            valueSlider.value = (float)arg;
            SliderValueChanged(valueSlider.value);

        }

        #endregion

        #region Private Fields

        private void SliderValueChanged(float newValue)
        {
            sliderValueLabel.SetText(valueSlider.value.ToString($"F{floatLength}"));
            SetupValue(newValue);
        }

        #endregion
    }
}
