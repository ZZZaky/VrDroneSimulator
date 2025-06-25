using TMPro;
using UnityEngine;

namespace FDS.GameScenarios.Checkpoints
{
    public class TimerUI : MonoBehaviour
    {
        #region Properties

        public TextMeshProUGUI TimerText { get; set; }

        public CheckpointRouteController CheckpointRouteController { get; set; }

        public CheckpointGameModeManager CheckpointGameModeManager { get; set; }

        #endregion

        #region LifeCycle

        private void Update()
        {
            TimerText.text = CheckpointRouteController.GetGameModeTime().ToString();
        }

        #endregion

        #region Public Methods

        public void ClearTimerUI()
        {
            TimerText.text = "";
        }

        #endregion
    }
}

