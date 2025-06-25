using FDS.GlitchShader;
using UnityEngine;

namespace FDS.Effects
{
    [RequireComponent(typeof(GlitchControll))]
    public class GlitchSignalEffect : SignalEffect
    {
        #region Fields

        private GlitchControll _effectControl;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _effectControl = GetComponent<GlitchControll>();
        }

        #endregion

        #region Public Methods

        public override void UpdateEffect(float signalPower)
        {
            _effectControl.SetIntensity(GetIntensity(signalPower));
        }

        #endregion

        #region Protected Methods

        protected virtual float GetIntensity(float signal)
        {
            return Mathf.Exp(-5.9f * signal);
        }

        #endregion
    }
}
