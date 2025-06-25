using UnityEngine;

namespace WireVR.Wires
{
    [RequireComponent(typeof(HingeJoint))]
    public class WireNode : MonoBehaviour
    {
        #region Fields

        private HingeJoint _hingeJoint;

        #endregion

        #region Properties

        public HingeJoint HingeJoint
        {
            get 
            {
                if (_hingeJoint == null)
                    _hingeJoint = GetComponent<HingeJoint>();
                
                return _hingeJoint;
            }
        }

        #endregion
    }
}