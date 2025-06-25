using ConstructionVR.Assembly.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace ConstructionVR.Assembly
{
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(BoxCollider))]
    public class ConnectableDetail : XRGrabInteractable, IConnection
    {
        #region Fields

        [SerializeField]
        private Detail typeDetail;

        private Outline _outline;

        private bool _isActiveDetail = true;
        private ConnectionStatys _status;

        #endregion

        #region Properties

        public Outline Outline => _outline;

        public Detail TypeDetail => typeDetail;

        public bool IsConnectable { get; private set; }

        public ConnectionStatys Status
        {
            get { return _status; }
            private set
            {
                _status = value;
                OnChangeStatys?.Invoke(_status, this);
            }
        }

        #endregion

        #region Events

        public UnityEvent<ConnectionStatys, ConnectableDetail> OnChangeStatys;

        #endregion

        #region LifeCycle

        protected override void Awake()
        {
            base.Awake();
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }

        private void Start()
        {
            Status = isSelected ? ConnectionStatys.Connected : ConnectionStatys.Disconnected;
        }

        #endregion

        #region PublicMethods

        public void Initialize(Detail detail)
        {
            typeDetail = detail;

            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().isKinematic = true;
            throwOnDetach = false;

            Status = ConnectionStatys.Disconnected;
        }

        public virtual bool CheckAngleBetweenAttach(Transform attachQuaternion)
        {
            if (!typeDetail.IsCheckAngle)
                return true;

            bool result = true;
            float angleX = 0, angleY = 0, angleZ = 0;

            //Check X
            if (typeDetail.AngleCheckX.IsCheck)
            {
                var proect = Vector3.ProjectOnPlane(transform.forward, Vector3.right);
                var proect2 = Vector3.ProjectOnPlane(attachQuaternion.forward, Vector3.right);

                angleX = Vector3.Angle(proect, proect2);
                angleX = typeDetail.AngleCheckX.Mirror ? (angleX > 90 ? 180 - angleX : angleX) : angleX;
                result &= angleX < typeDetail.AngleCheckX.Error;
            }

            //Check Y
            if (typeDetail.AngleCheckY.IsCheck)
            {
                var proect = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
                var proect2 = Vector3.ProjectOnPlane(attachQuaternion.forward, Vector3.up);

                angleY = Vector3.Angle(proect, proect2);
                angleY = typeDetail.AngleCheckY.Mirror ? (angleY > 90 ? 180 - angleY : angleY) : angleY;
                result &= angleY < typeDetail.AngleCheckY.Error;
            }

            //Check Z
            if (typeDetail.AngleCheckZ.IsCheck)
            {
                var proect = Vector3.ProjectOnPlane(transform.right, Vector3.forward);
                var proect2 = Vector3.ProjectOnPlane(attachQuaternion.right, Vector3.forward);

                angleZ = Vector3.Angle(proect, proect2);
                angleZ = typeDetail.AngleCheckZ.Mirror ? (angleZ > 90 ? 180 - angleZ : angleZ) : angleZ;
                result &= angleZ < typeDetail.AngleCheckZ.Error;
            }

            Debug.Log($"X = {angleX}, Y = {angleY}, Z = {angleZ}, Result = {result}");
            return result;
        }

        public void SetActive(bool isActive)
        {
            interactionLayers = isActive ? InteractionLayerMask.GetMask("Default")
                : InteractionLayerMask.GetMask("NotInteractable");

            _isActiveDetail = isActive;
        }

        #endregion

        #region ProtectedMethods

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);

            // _outline.enabled = _isActiveDetail;
        }

        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            base.OnHoverExited(args);
            //  _outline.enabled = false;
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            StopAllCoroutines();
            IsConnectable = true;
            // _outline.enabled = false;

            if (args.interactorObject.transform.TryGetComponent<IConnector>(out _))
                Status = ConnectionStatys.Connected;
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);

            if (isActiveAndEnabled)
            {
                StartCoroutine(SetConnectableWithDelay(false, 0.2f));
            }

            if (args.interactorObject.transform.TryGetComponent<IConnector>(out _))
            {
                Status = ConnectionStatys.Disconnected;
            }
        }

        #endregion

        #region PrivateMethods

        private IEnumerator SetConnectableWithDelay(bool isActive, float delay)
        {
            yield return new WaitForSeconds(delay);
            IsConnectable = isActive;
        }

        #endregion
    }
}
