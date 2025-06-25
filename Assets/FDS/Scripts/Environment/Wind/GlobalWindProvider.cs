using UnityEngine;

namespace FDS.Environment.Wind
{
    public class GlobalWindProvider : WindProvider
    {
        #region Fields

        [SerializeField]
        private EnvironmentParamsProvider envParamsProvider;

        private float _currentWindPhase;
        private float _currentAngle;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _currentAngle = Random.Range(0, 360);
            _currentWindPhase = 0;
            Wind = Quaternion.Euler(0, _currentAngle, 0) * Vector3.forward * envParamsProvider.Params.MaxGlobalWindPower
                * Mathf.Sin(2 * Mathf.PI * envParamsProvider.Params.GlobalWindOscilationFrequency * _currentWindPhase);
        }

        private void FixedUpdate()
        {
            _currentWindPhase += Time.deltaTime;
            _currentWindPhase %= 2 * Mathf.PI;
            Wind = RotateWind(GetWind(), _currentAngle + Random.Range(1f, -1f));
        }

        #endregion

        #region Private Methods

        private Vector3 GetWind()
        {
            return Vector3.forward * (envParamsProvider.Params.MaxGlobalWindPower * Mathf.Sin(2 * Mathf.PI 
                * envParamsProvider.Params.GlobalWindOscilationFrequency * _currentWindPhase) 
                + envParamsProvider.Params.MaxGlobalWindPower / 2);
        }

        private Vector3 RotateWind(Vector3 wind, float yAngle)
        {
            return Quaternion.Euler(0, yAngle, 0) * wind;
        }

        #endregion
    }
}