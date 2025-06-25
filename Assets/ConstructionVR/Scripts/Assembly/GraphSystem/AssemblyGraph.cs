using ConstructionVR.Assembly.Components;
using ConstructionVR.Assembly.Data;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNode;

namespace ConstructionVR.Assembly.GraphSystem
{
    [CreateAssetMenu]
    public class AssemblyGraph : NodeGraph
    {
        #region Constant

        private const int SpaceBetweenNode = 250;

        #endregion

        #region Fields

        [SerializeField]
        private string pathToAssemblyTemplete;

        [SerializeField]
        private string pathToAssemblyData;

        [SerializeField]
        private string pathToAssemblyBase;

        [SerializeField]
        private string pathToDetails;

        private string pathToConnectors;

        #endregion

        #region Properties

        public string PathToDetails => pathToDetails;

        public string PathToAssemblyBase => pathToAssemblyBase;

        public string PathToAssemblyData => pathToAssemblyData;

        public string PathToConnectors => pathToConnectors;

        #endregion

        #region PublicMethods

        public void Initialize(string pathToDetails, string pathToAssemblyBase, string pathToAssemblyData, string pathToConnectors)
        {
            this.pathToDetails = pathToDetails;
            this.pathToAssemblyBase = pathToAssemblyBase;
            this.pathToAssemblyData = pathToAssemblyData;
            this.pathToConnectors = pathToConnectors;
        }

        public bool ContainsDetailInNodes(Detail detail)
        {
            foreach (var node in nodes)
            {
                if((node as ConnectorTypeNode).CheckConnectedDetail(detail))
                {
                    return true;
                }
            }

            return false;
        }

        public ConnectorTypeNode CreateConnectorType(string name)
        {
            var node = AddNode<ConnectorTypeNode>();
            node.position = new Vector2(SpaceBetweenNode * nodes.Count, 0);
            node.name = name;

            if (nodes.Count > 1)
            {
                (nodes[nodes.Count - 2] as ConnectorTypeNode).GetDependentPort().Connect(node.GetConnectedPort());
            }

            return node;
        }

        public void UpdatePositionNode()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].position = new Vector2(SpaceBetweenNode * i, 0);
            }
        }

        public void Initialize(Dictionary<ConnectorTypeNode, List<DetailMarker>> detailsMarkerGroup)
        {
            var queueToRemove = new List<Node>();

            for (int i = 0; i < nodes.Count; i++)
            {
                if (!detailsMarkerGroup.ContainsKey(nodes[i] as ConnectorTypeNode))
                {
                    queueToRemove.Add(nodes[i]);
                }
            }

            foreach (var node in queueToRemove)
            {
                RemoveNode(node);
            }

            UpdatePositionNode();
        }

        public override Node AddNode(Type type)
        {
            var node = base.AddNode(type);

#if UNITY_EDITOR
            AssetDatabase.GetAssetPath(this);
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
#endif

            return node;
        }

        public override void RemoveNode(Node node)
        {
            base.RemoveNode(node);
#if UNITY_EDITOR
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
#endif
        }

        public override void Clear()
        {
#if UNITY_EDITOR
            for (int i = 0; i < nodes.Count; i++)
            {
                AssetDatabase.RemoveObjectFromAsset(nodes[i]);
            }

            AssetDatabase.SaveAssets();
#endif
            nodes.Clear();
        }

        #endregion
    }
}