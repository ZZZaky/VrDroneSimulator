using UnityEngine;

namespace ConstructionVR.Errors
{
    public abstract class Error : MonoBehaviour
    {
        [SerializeField]
        private Transform offset;

        public Transform Offset
        {
            get
            {
                if (offset == null)
                    return transform;

                return offset;
            }
        }

        public abstract bool TryGetError(out string errorMessage);
    }
}
