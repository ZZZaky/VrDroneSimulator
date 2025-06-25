using System.Collections.Generic;
using UnityEngine;

namespace ConstructionVR.Assembly
{
    [System.Serializable]
    public struct DetailMeshGroup
    {
        public List<MeshFilter> meshRenderers;

        public DetailMeshGroup(MeshFilter firstMeshFilter)
        {
            meshRenderers = new List<MeshFilter>();
            meshRenderers.Add(firstMeshFilter);
        }
    }
}