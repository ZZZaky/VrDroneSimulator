using UnityEngine;

namespace FDS.UI
{
    public class HorizonLine : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Drone.Drone drone;

        [SerializeField]
        private float yOffsetScale;

        [SerializeField]
        private float maxYOffset;

        private Vector3 _startPosition;

        #endregion

        #region LifeCycle

        private void Start()
        {
            _startPosition = transform.localPosition;
        }

        private void Update()
        {
            var droneAngles = drone.ReadCurrentAngles();

            var rotation = Quaternion.Euler(0, 0, -droneAngles.Roll);
            var offset = Mathf.Clamp(Mathf.Sin(-droneAngles.Pitch * Mathf.Deg2Rad) * yOffsetScale, -maxYOffset, maxYOffset);

            transform.localPosition = _startPosition + Vector3.up * offset;
            transform.localRotation = rotation;
        }

        #endregion
    }
}
