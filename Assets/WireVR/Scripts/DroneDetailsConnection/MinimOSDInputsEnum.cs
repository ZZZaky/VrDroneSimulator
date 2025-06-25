using UnityEngine;

namespace WireVR.Connection
{
    public enum MinimOSDInputType
    {
        TelemInput,
        CameraOrTBSInput
    }

    public class MinimOSDInputsEnum : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private MinimOSDInputType minimOSDInputType;

        #endregion

        #region Properties

        public MinimOSDInputType MinimOSDInputType => minimOSDInputType;

        #endregion
    }
}