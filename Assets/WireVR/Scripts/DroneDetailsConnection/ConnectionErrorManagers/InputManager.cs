using ConstructionVR.Assembly;
using ConstructionVR.Assembly.Data;
using ConstructionVR.Errors;
using UnityEngine;
using WireVR.Wires;

namespace WireVR.Connection
{
    public abstract class InputManager : Error
    {
        #region Fields

        [SerializeField]
        protected WireInput input;

        [SerializeField]
        protected ConnectableDetail connectableDetail;

        #endregion

        #region Properties

        public WireInput WireInput => input;

        public Detail CurrentDetail => connectableDetail.TypeDetail;

        #endregion

        #region Public Methods

        public override abstract bool TryGetError(out string errorMessage);

        #endregion
    }
}