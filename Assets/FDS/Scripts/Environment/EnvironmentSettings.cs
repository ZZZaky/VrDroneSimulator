using UnityEngine;

namespace FDS.Environment
{
    [CreateAssetMenu(fileName = "EnvironmentSettings", menuName = "Drone/Environment/Settings")]

    public class EnvironmentSettings : ScriptableObject
    {
        #region Fields

        [SerializeField]
        private float aerodynamicThrustCoefficient;

        [SerializeField]
        private float maxGlobalWindPower;

        [SerializeField]
        private float globalWindOscilationFrequency;

        #endregion

        #region Properties

        public float AerodynamicThrustCoefficient { get => aerodynamicThrustCoefficient; set => aerodynamicThrustCoefficient = value; }
        public float MaxGlobalWindPower { get => maxGlobalWindPower; set => maxGlobalWindPower = value; }
        public float GlobalWindOscilationFrequency { get => globalWindOscilationFrequency; set => globalWindOscilationFrequency = value; }

        #endregion
    }
}