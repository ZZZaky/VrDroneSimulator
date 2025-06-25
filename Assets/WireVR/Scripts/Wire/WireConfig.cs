using System;
using UnityEngine;

namespace WireVR.Wires
{
    [Serializable]
    public struct WireConfig
    {
        #region Fields

        [Header("Line Rendering")]
        public float density;

        public float lineWight;

        public Gradient wireGradient;

        [Header("Wire Part Spawn")]
        public WireOutput startWireOutput;

        public WireOutput endWireOutput;

        public WireRenderer currentWireRenderer;

        #endregion
    }
}