using ConstructionVR.Assembly.Data;
using WireVR.Wires;

namespace WireVR.Connection
{
    public struct MiniOSDInputConfig
    {
        public Detail detail;

        public MinimOSDInputType minimOSDInputType;
    }

    public class ConnectableObjectWithEnum : ConnectableObject
    {
        #region Protected Methods

        protected override void OnChangeConnected(WireInput wireInput, Wire wire, Detail detail)
        {
            if (wire != null)
            {
                MiniOSDInputConfig inputConfig = new MiniOSDInputConfig();

                inputConfig.detail = connectableDetail.TypeDetail;
                inputConfig.minimOSDInputType = wireInput.GetComponent<MinimOSDInputsEnum>().MinimOSDInputType;

                wire.Data = inputConfig;
            }
        }

        #endregion
    }
}