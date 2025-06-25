using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace ConstructionVR.SubInteractableSystem.RotatorGrab
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class Rotator : MonoBehaviour
    {
        #region Field

        [SerializeField]
        private Transform rotatorOffset;

        [Header("Параметры")]
        [SerializeField]
        private int snapRotationAmount = 2;

        [SerializeField]
        private float angleTolerance = 1;

        [SerializeField]
        private bool isStartRandomRotate = true;

        private XRGrabInteractable _grabInteractor;
        private IXRSelectInteractor _interactor;

        private float _startAngle;

        private bool _requiresStartAngle = true;
        private bool _shouldGetHandRotation = false;

        #endregion

        #region Properties

        public XRGrabInteractable GrabInteractor
        {
            get
            {
                if (_grabInteractor == null)
                    _grabInteractor = GetComponent<XRGrabInteractable>();

                return _grabInteractor;
            }
        }

        #endregion

        #region Events

        public UnityEvent<float> OnUpdateAngles;
        public UnityEvent OnStartRotation;
        public UnityEvent OnStopRotation;

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            GrabInteractor.selectEntered.AddListener(OnSelectEntered);
            GrabInteractor.selectExited.AddListener(OnSelectExited);
        }

        private void OnDisable()
        {
            GrabInteractor.selectEntered.RemoveListener(OnSelectEntered);
            GrabInteractor.selectExited.RemoveListener(OnSelectExited);
        }

        private void Start()
        {
            if (isStartRandomRotate)
            {
                SetAngleZTargetObject(UnityEngine.Random.Range(0, 360));
            }
        }

        private void Update()
        {
            if (_shouldGetHandRotation)
            {
                var currentAngle = GetInteractorRotation();
                GetRotationDistance(currentAngle);
            }
        }

        #endregion

        #region PublicMethods

        public float GetInteractorRotation() => _interactor.transform.eulerAngles.z;

        public Vector3 GetCurrentEulerAngle() => rotatorOffset.localEulerAngles;

        #endregion

        #region PrivateMethods

        private float ConvertAngle(float currentAngle, float startAngle)
        {
            return (360f - currentAngle) + startAngle;
        }

        private void OnSelectEntered(SelectEnterEventArgs selectArgs)
        {
            _interactor = selectArgs.interactorObject;

            if (_interactor.transform.TryGetComponent<XRDirectInteractor>(out var xRDirectInteractor))
                xRDirectInteractor.hideControllerOnSelect = true;

            _shouldGetHandRotation = true;
            _startAngle = 0f;

            OnStartRotation?.Invoke();
        }

        private void OnSelectExited(SelectExitEventArgs selectArgs)
        {
            _shouldGetHandRotation = false;
            _requiresStartAngle = true;
            OnStopRotation?.Invoke();
        }

        private void GetRotationDistance(float currentAngle)
        {
            if (_startAngle == currentAngle)
                return;

            var angleDifference = Mathf.Abs(_startAngle - currentAngle);

            if (angleDifference <= angleTolerance)
                return;

            if (_requiresStartAngle)
            {
                _requiresStartAngle = false;
                _startAngle = currentAngle;
                return;
            }

            if (angleDifference > 270f)
            {
                if (ConvertAngle(currentAngle, _startAngle) < angleTolerance)
                    return;
            }

            if (_startAngle < currentAngle)
            {
                RotateControlClockwise();
            }
            else
            {
                RotateClockwise();
            }

            _startAngle = currentAngle;
        }

        private void SetAngleZTargetObject(float angleZ)
        {
            rotatorOffset.localEulerAngles = new Vector3(rotatorOffset.localEulerAngles.x,
                rotatorOffset.localEulerAngles.y, angleZ);

            OnUpdateAngles?.Invoke(angleZ);
        }

        private void RotateClockwise()
        {
            var newAngleZ = rotatorOffset.localEulerAngles.z - snapRotationAmount;
            SetAngleZTargetObject(newAngleZ);
        }

        private void RotateControlClockwise()
        {
            var newAngleZ = rotatorOffset.localEulerAngles.z + snapRotationAmount;
            SetAngleZTargetObject(newAngleZ);
        }

        #endregion
    }
}
