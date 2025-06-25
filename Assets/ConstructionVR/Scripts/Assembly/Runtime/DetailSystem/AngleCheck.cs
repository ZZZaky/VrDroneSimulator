using UnityEngine;

namespace ConstructionVR.Assembly.Data
{
    [System.Serializable]
    public struct AngleCheck
    {
        public bool IsCheck;

        [Range(0f, 180f)]
        public float Error;

        public bool Mirror;
    }
}
