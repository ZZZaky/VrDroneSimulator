using UnityEngine;
using UnityEngine.UI;

namespace FDS.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class HomeArrow : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Drone.Drone drone;

        private RectTransform _rectTransform;
        private Image _image;
        private Vector3 _target;
        private bool _isEnabled = false;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
            _image.enabled = _isEnabled;
        }

        private void Update()
        {
            if(_isEnabled && _target != null)
            {
                var delta = _target - drone.BodyTransform.position;
                var forward = drone.BodyTransform.right;
                delta.y = 0;
                forward.y = 0;

                var homeAngleY = Vector3.SignedAngle(delta, forward, Vector3.up);
                _rectTransform.localRotation = Quaternion.AngleAxis(homeAngleY, Vector3.forward);
            }
            
        }

        #endregion

        #region Public Methods

        public void ToggleArrow(bool isEnabled)
        {
            _image.enabled = isEnabled;
            _isEnabled = isEnabled;
        }

        public void SetTarget(Vector3 target)
        {
            _target = target;
        }

        #endregion

    }
}