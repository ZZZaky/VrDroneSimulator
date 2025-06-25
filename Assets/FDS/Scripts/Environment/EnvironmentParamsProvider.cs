using UnityEngine;

namespace FDS.Environment
{
    public class EnvironmentParamsProvider : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private EnvironmentSettings envParams;

        #endregion

        #region Properties

        public EnvironmentSettings Params => envParams;

        #endregion
    }
}