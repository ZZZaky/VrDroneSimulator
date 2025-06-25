using FDS.Interfaces;
using UnityEngine;

namespace FDS.Drone
{
    [RequireComponent(typeof(Animator))]
    public class DronePropellerAnimator : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Drone drone;

        private Animator _animator;

        #endregion

        #region LyfeCycle

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            drone.OnDroneDisabled += DisableRotation;
            drone.OnDroneEnabled += EnableRotation;
        }

        private void OnDestroy()
        {
            drone.OnDroneDisabled -= DisableRotation;
            drone.OnDroneEnabled -= EnableRotation;
        }

        private void OnEnable()
        {
            if (drone.IsDroneEnabled)
                EnableRotation();
            else
                DisableRotation();
        }

        #endregion

        #region Public Methods

        public void EnableRotation()
        {
            _animator.SetBool("IsOn", true);
        }

        public void DisableRotation()
        {
            _animator.SetBool("IsOn", false);
        }

        #endregion
    }
}