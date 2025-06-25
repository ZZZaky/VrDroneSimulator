using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;
using Zenject.SpaceFighter;
using Zenject;

namespace Exposition.Players.General.LookAt
{
    public class LookAtObject : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private bool autoRegistration = true;

        [SerializeField]
        private bool isInverse = true;

        [SerializeField]
        private bool isIgnoreX = false;

        [SerializeField]
        private bool isIgnoreY = true;

        [SerializeField]
        private bool isIgnoreZ = false;

        private Transform _target;

        #endregion

        #region Constructors

        [Inject]
        private void Construct(PlayerLinker playerLink)
        {
            _target = playerLink.Player.transform;

           
        }

        #endregion

        #region LifeCycle

        private void Start()
        {
            if (autoRegistration)
            {
                FindObjectOfType<LookAtSeter>().RegistrationLookAtObject(this);
            }
        }

        private void Update()
        {
            if (_target == null)
                return;

            var lookAt = new Vector3()
            {
                x = isIgnoreX ? transform.position.x : _target.position.x,
                y = isIgnoreY ? transform.position.y : _target.position.y,
                z = isIgnoreZ ? transform.position.z : _target.position.z,
            };

            if (isInverse)
            {
                lookAt = transform.position - (lookAt - transform.position);
            }

            transform.LookAt(lookAt);
        }

        #endregion

        #region PublicMethods

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        #endregion
    }
}
