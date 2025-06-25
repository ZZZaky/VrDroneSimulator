using ConstructionVR.Assembly.GraphSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

namespace ConstructionVR.Assembly
{
    [RequireComponent(typeof(BoxCollider))]
    public class DetailConnector : XRSocketInteractor, IConnection, IConnector
    {
        #region Constant

        public const string InteractionLayerName = "Default";
        public const string NotInteractionLayerName = "NotInteractable";

        #endregion

        #region Fields

        /// <summary>
        /// Тип к которому относится текущий конектор
        /// </summary>
        [SerializeField]
        private ConnectorTypeNode connectorType;

        /// <summary>
        /// Если значение true скрывает mesh конектера при старте
        /// </summary>
        [SerializeField]
        private bool isAutoHideMesh = true;

        [SerializeField]
        private Material stepMaterial;

        private bool _isActiveStep = false;
        private MeshRenderer _renderer;
        private ConnectionStatys _currentStatus = ConnectionStatys.Disconnected;
        private DetailConnector _parentConnector;
        private bool _isCheckAngles = false;
        private bool _hasAnglesComplete = false;

        #endregion

        #region Events

        public UnityEvent<ConnectionStatys, DetailConnector> OnChangeStatys;

        #endregion

        #region Properties

        public bool IsChildConnector => _parentConnector != null;

        public ConnectorTypeNode ConnectorType => connectorType;

        public ConnectableDetail ConnectedDetail { get; private set; }

        public bool IsAutoBuild { get; set; }

        public ConnectionStatys Status
        {
            get { return _currentStatus; }
            private set
            {
                _currentStatus = value;
                OnChangeStatys?.Invoke(_currentStatus, this);
            }
        }

        #endregion

        #region LifeCycle

        protected override void Awake()
        {
            base.Awake();
            if (isAutoHideMesh)
            {
                if (TryGetComponent<MeshRenderer>(out var renderer))
                {
                    renderer.enabled = false;
                    _renderer = renderer;
                }
                else
                {
                    Debug.LogWarning("Not found mesh renderer");
                }
            }

            _currentStatus = ConnectionStatys.Disconnected;
            CreateStepMaterial();
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="type"> Тип к которому будет относится этот конектор</param>
        public void Initialize(ConnectorTypeNode type)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            this.connectorType = type;
            //GetComponent<MeshRenderer>().enabled = false;
        }

        public void SetActiveConnector(bool isActive)
        {
            if (ConnectedDetail != null)
                ConnectedDetail.SetActive(isActive);

            interactionLayers = isActive ? InteractionLayerMask.GetMask(InteractionLayerName)
                : InteractionLayerMask.GetMask(NotInteractionLayerName);
        }

        public override bool CanHover(IXRHoverInteractable interactable)
        {
            if (Status == ConnectionStatys.Connected || IsAutoBuild)
            {
                return base.CanHover(interactable);
            }

            return base.CanHover(interactable)
            && CheckDetail(interactable.transform.gameObject, true);
        }

        public override bool CanSelect(IXRSelectInteractable interactable)
        {
            if (Status == ConnectionStatys.Connected || IsAutoBuild)
                return base.CanSelect(interactable);

            return base.CanSelect(interactable) && CheckDetail(interactable.transform.gameObject);
        }

        public void SetParentConnector(DetailConnector parentConnector)
        {
            _parentConnector = parentConnector;
        }

        public void ShowHideStepMaterial(bool isShow)
        {
            if (isShow)
            {
                if (_isActiveStep)
                    _renderer.enabled = true;
            }
            else
            {
                _renderer.enabled = false;
            }
        }

        public void SetActiveStepMaterial(bool isActive)
        {
            if (_renderer == null)
            {
                if (!TryGetComponent(out _renderer))
                    return;
            }

            _isActiveStep = isActive;

            if (Status == ConnectionStatys.Connected)
            {
                _renderer.enabled = false;
                return;
            }

            if (_isActiveStep)
            {
                _renderer.material = stepMaterial;
                _renderer.enabled = isActive;
            }
        }

        public void SetCheckAngles(bool isActive)
        {
            _isCheckAngles = isActive;
        }

        #endregion

        #region ProtectedMethods

        protected override bool ShouldDrawHoverMesh(MeshFilter meshFilter, Renderer meshRenderer, Camera mainCamera)
        {
            if (meshFilter.TryGetComponent<IgnoreDrawMesh>(out _))
            {
                return false;
            }

            return base.ShouldDrawHoverMesh(meshFilter, meshRenderer, mainCamera);
        }

        protected override Material GetHoveredInteractableMaterial(IXRHoverInteractable interactable)
        {
            if (_isCheckAngles)
            {
                return !_hasAnglesComplete ? interactableCantHoverMeshMaterial : interactableHoverMeshMaterial;
            }

            return hasSelection ? interactableCantHoverMeshMaterial : interactableHoverMeshMaterial;
        }

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);

            if (_isActiveStep)
            {
                _renderer.enabled = false;
            }
        }

        protected override void OnHoverExiting(HoverExitEventArgs args)
        {
            base.OnHoverExiting(args);

            if (_isActiveStep && ConnectionStatys.Connected != Status)
            {
                _renderer.enabled = true;
            }
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            allowHover = false;
            Status = ConnectionStatys.Connected;

            _renderer.enabled = false;
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            allowHover = true;
            Status = ConnectionStatys.Disconnected;
            ConnectedDetail = null;

            if (_isActiveStep)
            {
                _renderer.enabled = true;
            }
        }

        #endregion

        #region PrivateMethods

        private void CreateStepMaterial()
        {
            if (stepMaterial != null)
                return;

            var shaderName = GraphicsSettings.currentRenderPipeline ? "Universal Render Pipeline/Lit" : "Standard";
            var defaultShader = Shader.Find(shaderName);

            if (defaultShader == null)
            {
                Debug.LogWarning("Failed to create default materials for Socket Interactor," +
                    $" was unable to find \"{shaderName}\" Shader. Make sure the shader is included into the game build.", this);
                return;
            }

            stepMaterial = new Material(defaultShader);
            SetMaterialFade(stepMaterial, new Color(0f, 0f, 0f, 0.2f));
        }

        private static void SetMaterialFade(Material material, Color color)
        {
            material.SetOverrideTag("RenderType", "Transparent");
            material.SetFloat(ShaderPropertyLookup.mode, 2f);
            material.SetInt(ShaderPropertyLookup.srcBlend, (int)BlendMode.SrcAlpha);
            material.SetInt(ShaderPropertyLookup.dstBlend, (int)BlendMode.OneMinusSrcAlpha);
            material.SetInt(ShaderPropertyLookup.zWrite, 0);
            // ReSharper disable StringLiteralTypo
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            // ReSharper restore StringLiteralTypo
            material.renderQueue = (int)RenderQueue.Transparent;
            material.SetColor(GraphicsSettings.currentRenderPipeline ? ShaderPropertyLookup.baseColor : ShaderPropertyLookup.color, color);
        }

        private bool CheckDetail(GameObject detailObject, bool isHoverCheck = false)
        {
            if (!detailObject.TryGetComponent<ConnectableDetail>(out var connectableDetail))
                return false;

            if (!connectableDetail.IsConnectable)
                return false;

            if (!connectorType.CheckConnectedDetail(connectableDetail.TypeDetail))
                return false;

            if (_isCheckAngles)
            {
                _hasAnglesComplete = connectableDetail.CheckAngleBetweenAttach(attachTransform);

                if (!isHoverCheck)
                {
                    if (!_hasAnglesComplete)
                        return false;
                }
            }

            if (isHoverCheck)
                return connectableDetail.Status == ConnectionStatys.Disconnected;

            ConnectedDetail = connectableDetail;
            return true;
        }

        private struct ShaderPropertyLookup
        {
            public static readonly int mode = Shader.PropertyToID("_Mode");
            public static readonly int srcBlend = Shader.PropertyToID("_SrcBlend");
            public static readonly int dstBlend = Shader.PropertyToID("_DstBlend");
            public static readonly int zWrite = Shader.PropertyToID("_ZWrite");
            public static readonly int baseColor = Shader.PropertyToID("_BaseColor");
            public static readonly int color = Shader.PropertyToID("_Color"); // Legacy
        }

        #endregion
    }
}
