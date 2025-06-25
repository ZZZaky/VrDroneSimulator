using UnityEngine;

namespace ConstructionVR.CheckerUtilitis
{
    public class ZoneDetection : MonoBehaviour
    {
        #region NestedType

        public enum Zone
        {
            One,
            Two,
            Three,
            Four
        }

        #endregion

        #region Fields

        [SerializeField]
        private Transform attachZoneDetection;

        #endregion

        #region Properties

        public Transform AttachZoneDetection
        {
            get
            {
                if (attachZoneDetection == null)
                    attachZoneDetection = transform;

                return attachZoneDetection;
            }
        }

        #endregion

        #region PublicMethods

        public Zone GetZoneFromPostion(Vector3 worldPosition)
        {
            var dir = worldPosition - attachZoneDetection.position;
            var angle = Vector3.SignedAngle(dir, AttachZoneDetection.forward, Vector3.up);

            if (angle <= 0 && angle >= -90)
            {
                return Zone.One;
            }

            if (angle >= 90 && angle <= 180)
            {
                return Zone.Two;
            }

            if (angle >= 0 && angle <= 90)
            {
                return Zone.Three;
            }

            if (angle <= -90 && angle >= -180)
            {
                return Zone.Four;
            }

            return Zone.One;
        }

        #endregion

        #region Debug

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Vector3 cubeSize = Vector3.one / 4;

            var poseZone1 = AttachZoneDetection.position + AttachZoneDetection.forward * cubeSize.x + AttachZoneDetection.right * cubeSize.x;
            var poseZone3 = AttachZoneDetection.position + AttachZoneDetection.forward * cubeSize.x - AttachZoneDetection.right * cubeSize.x;
            var poseZone4 = AttachZoneDetection.position - AttachZoneDetection.forward * cubeSize.x + AttachZoneDetection.right * cubeSize.x;
            var poseZone2 = AttachZoneDetection.position - AttachZoneDetection.forward * cubeSize.x - AttachZoneDetection.right * cubeSize.x;

            UnityEditor.Handles.color = Color.white;
            UnityEditor.Handles.Label(poseZone1, "Zone 1 (CCW)");
            UnityEditor.Handles.Label(poseZone2, "Zone 2 (CCW)");
            UnityEditor.Handles.Label(poseZone3, "Zone 3 (CW)");
            UnityEditor.Handles.Label(poseZone4, "Zone 4 (CW)");

            Gizmos.color = new Color(0, 0, 0, 0.5f);

            Gizmos.DrawCube(poseZone1, cubeSize);
            Gizmos.DrawCube(poseZone2, cubeSize);
            Gizmos.DrawCube(poseZone3, cubeSize);
            Gizmos.DrawCube(poseZone4, cubeSize);
        }
#endif
        #endregion
    }
}
