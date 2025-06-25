using UnityEngine;

namespace FDS.Controls
{
    public class ButtonForChangeControl : BaseButton
    {
        #region Fields

        [SerializeField]
        private StateMachine.StateMachine controlsStateMachine;

        #endregion

        #region Public Methods
        public void ChangeMode()
        {
            controlsStateMachine.NextDefaultState();
            Debug.Log("ChangeMode!");
        }
        #endregion
    }
}
