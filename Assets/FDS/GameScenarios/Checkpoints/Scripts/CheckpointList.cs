using System.Collections.Generic;
using UnityEngine;

namespace FDS.GameScenarios.Checkpoints
{
    public class CheckpointList : MonoBehaviour
    {

        #region Fields

        [SerializeField] private List<Checkpoint> checkpoints;

        #endregion

        #region Properties

        public List<Checkpoint> CheckpointsList
        {
            get
            {
                return checkpoints;
            }
        }

        #endregion

        #region Public Methods

        public Checkpoint GetCheckpoint(int index)
        {
            if (index < 0 | index > checkpoints.Count) return null;
            return checkpoints[index];
        }

        [ContextMenu("Добавить Чекпоинты")]
        private void AddCheckpointsToList()
        {
            checkpoints ??= new List<Checkpoint>();
            byte id = 0;

            for (int i = 0; i < transform.childCount; i++)
            {
                var checkpoint = transform.GetChild(i).GetComponentInChildren<Checkpoint>();
                if (checkpoint == null)
                    continue;

                if (checkpoints.Contains(checkpoint))
                    continue;

                id++;
                checkpoints.Add(checkpoint);
                checkpoint.gameObject.name = id.ToString();
            }
        }
        #endregion

    }
}

