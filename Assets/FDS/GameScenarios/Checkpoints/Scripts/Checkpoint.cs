using System;
using Tools;
using UnityEngine;

namespace FDS.GameScenarios.Checkpoints
{
    public class Checkpoint : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private LayerMask interactionLayer;

        #endregion

        #region Properties

        public int ID { get; set; }

        public bool IsActive
        {
            get
            {
                return gameObject.activeInHierarchy;
            }
            set
            {
                gameObject.SetActive(value);
            }
        }

        #endregion

        #region Events

        public event Action<Checkpoint> OnFlight;

        #endregion

        #region LifeCycle

        private void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsLayerInMask(interactionLayer, other.gameObject.layer))
            {
                OnFlight?.Invoke(this);
            }
        }

        #endregion

    }
}

