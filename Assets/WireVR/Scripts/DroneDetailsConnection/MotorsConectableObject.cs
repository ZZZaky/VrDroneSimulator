using ConstructionVR.Assembly.Data;
using UnityEngine;
using WireVR.Wires;

namespace WireVR.Connection
{
    public struct MotorsInputConfig
    {
        public Transform transform;

        public Detail detail;
    }

    public class MotorsConectableObject : ConnectableObject
    {
        #region Protected Methods

        protected override void OnChangeConnected(WireInput wireInput, Wire wire, Detail detail)
        {
            if (wire != null && wire.Data == null)
            {
                MotorsInputConfig inputConfig = new MotorsInputConfig();

                inputConfig.transform = transform;
                inputConfig.detail = connectableDetail.TypeDetail;

                wire.Data = inputConfig;
            }
        }

        #endregion
    }
}