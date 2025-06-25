using System.Collections.Generic;
using UnityEngine;

namespace FDS.Environment.Wind
{
    public class WindManager : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private WindProvider globalWindProvider;

        [SerializeField]
        private Rigidbody allowedTarget;

        private Stack<WindProvider> _activeWinds = new Stack<WindProvider>();

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _activeWinds.Push(globalWindProvider);
        }

        private void FixedUpdate()
        {
            var wind = _activeWinds.Peek();

            allowedTarget.AddForce(wind.Wind);
        }

        #endregion

        #region Public Methods

        public bool IsAllowedTarget(Rigidbody rb)
        {
            return rb == allowedTarget;
        }

        public void ApplyLocalWind(WindProvider wind, Rigidbody target)
        {
            _activeWinds.Push(wind);
        }

        public void ResetLocalWind(Rigidbody target)
        {
            _activeWinds.Pop();
        }

        #endregion
    }
}