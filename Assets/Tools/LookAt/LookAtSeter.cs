using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Zenject;

namespace Exposition.Players.General.LookAt
{
    public class LookAtSeter : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Transform target;

        #endregion

        #region Constructors

        [Inject]
        private void Construct(PlayerLinker playerLink)
        {
            target = playerLink.Player.transform;


        }

        #endregion

        #region LifeCycle

        private void Awake()
        {
            Initialize();
        }

        #endregion

        #region PublicMethods

        public void RegistrationLookAtObject(LookAtObject lookAtObject)
        {
            lookAtObject.SetTarget(target);
        }

        #endregion

        #region PrivateMethods

        private void Initialize()
        {
            var lookObjects = FindObjectsOfType<LookAtObject>();

            foreach (var lookObject in lookObjects)
            {
                lookObject.SetTarget(target);
            }
        }

        #endregion
    }
}
