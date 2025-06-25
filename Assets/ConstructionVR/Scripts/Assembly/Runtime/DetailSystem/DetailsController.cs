using ConstructionVR.Assembly.GraphSystem;
using System.Collections.Generic;
using UnityEngine;

namespace ConstructionVR.Assembly.Data
{
    public class DetailsController : MonoBehaviour
    {
        #region Fields

        private Dictionary<Detail, List<ConnectableDetail>> _disconnectedDetails = new();
        private Dictionary<Detail, List<ConnectableDetail>> _connectedDetails = new();

        #endregion

        #region PublicMethods

        public void Initialize(AssemblyGraph assemblyGraph)
        {
            var allDetailsInWorld = FindObjectsOfType<ConnectableDetail>();
            foreach (var detail in allDetailsInWorld)
            {
                if (assemblyGraph.ContainsDetailInNodes(detail.TypeDetail))
                {
                    AddDetail(detail);
                }
            }
        }

        public ConnectableDetail GetDisconnectedDetail(Detail typeDetail)
        {
            if (_disconnectedDetails.ContainsKey(typeDetail))
            {
                var connectableDetails = _disconnectedDetails[typeDetail];


                if (connectableDetails.Count > 0)
                {
                    var connectableDetail = connectableDetails[connectableDetails.Count - 1];
                    _disconnectedDetails[typeDetail].Remove(connectableDetail);
                    return connectableDetail;
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region PrivateMethods

        private void AddDetail(ConnectableDetail connectableDetail)
        {
            if (connectableDetail.Status == ConnectionStatys.Connected)
            {
                AddDetailInDictionary(connectableDetail, _connectedDetails);
            }
            else if (connectableDetail.Status == ConnectionStatys.Disconnected)
            {
                AddDetailInDictionary(connectableDetail, _disconnectedDetails);
            }

            connectableDetail.OnChangeStatys.AddListener(OnChangeStatysDetail);
        }

        private void OnChangeStatysDetail(ConnectionStatys statys, ConnectableDetail connectableDetail)
        {
            if (connectableDetail.Status == ConnectionStatys.Connected)
            {
                if (_disconnectedDetails.ContainsKey(connectableDetail.TypeDetail))
                    _disconnectedDetails[connectableDetail.TypeDetail].Remove(connectableDetail);

                AddDetailInDictionary(connectableDetail, _connectedDetails);
            }
            else if (connectableDetail.Status == ConnectionStatys.Disconnected)
            {
                if (_connectedDetails.ContainsKey(connectableDetail.TypeDetail))
                    _connectedDetails[connectableDetail.TypeDetail].Remove(connectableDetail);

                AddDetailInDictionary(connectableDetail, _disconnectedDetails);
            }
        }

        private void AddDetailInDictionary(ConnectableDetail connectableDetail,
            Dictionary<Detail, List<ConnectableDetail>> dictionary)
        {
            if (!dictionary.ContainsKey(connectableDetail.TypeDetail))
            {
                dictionary.Add(connectableDetail.TypeDetail, new());
            }

            dictionary[connectableDetail.TypeDetail].Add(connectableDetail);
        }


        #endregion

    }
}