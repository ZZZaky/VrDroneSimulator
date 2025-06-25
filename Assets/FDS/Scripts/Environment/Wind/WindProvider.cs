using UnityEngine;

namespace FDS.Environment.Wind
{
    public class WindProvider : MonoBehaviour
    {
        #region Properties

        public Vector3 Wind { get; protected set; }

        #endregion

        #region Public Methods

        public void ApplyTo(Rigidbody rb)
        {
            rb.AddForce(Wind);
        }

        #endregion

    }
}