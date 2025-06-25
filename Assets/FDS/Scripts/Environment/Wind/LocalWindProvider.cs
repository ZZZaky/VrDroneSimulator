using UnityEngine;

namespace FDS.Environment.Wind
{
    public class LocalWindProvider : WindProvider
    {
        #region Fields

        [SerializeField]
        private WindManager windManager;

        [SerializeField]
        private Vector3 windPower;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            Wind = windPower;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Rigidbody>(out var rb) && windManager.IsAllowedTarget(rb))
            {
                windManager.ApplyLocalWind(this, rb);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Rigidbody>(out var rb) && windManager.IsAllowedTarget(rb))
            {
                windManager.ResetLocalWind(rb);
            }
        }

        #endregion
    }
}