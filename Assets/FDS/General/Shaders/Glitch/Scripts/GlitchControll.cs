using UnityEngine;

namespace FDS.GlitchShader
{
    public class GlitchControll : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Material glitchMaterial;

        #endregion

        #region PublicMethods

        /// <summary>
        /// SetIntensity glitch material
        /// </summary>
        /// <param name="value">Between [0:1]</param>
        public void SetIntensity(float value)
        {
            value = Mathf.Clamp01(value);
            glitchMaterial.SetFloat("_Intensity", value);
        }

        #endregion

        #region Debug

        [ContextMenu("Set 0")]
        public void Set0()
        {
            SetIntensity(0);
        }

        [ContextMenu("Set 0.5")]
        public void Set05()
        {
            SetIntensity(0.5f);
        }

        [ContextMenu("Set 1")]
        public void Set1()
        {
            SetIntensity(1);
        }

        #endregion
    }
}