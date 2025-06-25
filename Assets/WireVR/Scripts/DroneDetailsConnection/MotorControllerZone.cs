using UnityEngine;
using static ConstructionVR.CheckerUtilitis.ZoneDetection;

namespace WireVR.Connection
{
    public class MotorControllerZone : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Zone zone;
        
        #endregion

        #region Properties

        public Zone Zone => zone;

        #endregion
    }
}