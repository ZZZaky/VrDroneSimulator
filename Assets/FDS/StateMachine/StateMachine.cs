using System.Collections.Generic;
using UnityEngine;

namespace FDS.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private State state;

        #endregion

        #region Properties

        public State CurrentState => state;

        #endregion

        #region LifeCycle

        private void Start()
        {
            state?.OnEnter();
        }

        #endregion

        #region Public Methods

        public void SetTrigger(string triggerName)
        {
            var newState = CurrentState.GetNextState(triggerName);
            ChangeCurrentState(newState);
        }
        public void NextDefaultState()
        {
            var newState = CurrentState.GetDefaultNextState();
            ChangeCurrentState(newState);
            Debug.Log("NextDefaultState");
        }

        public void ExecuteCurrent()
        {
            CurrentState.Execute();
        }

        #endregion

        #region Private Methods

        private void ChangeCurrentState(State newState)
        {
            CurrentState.OnExit();
            newState.OnEnter();
            Debug.Log(state);
            state = newState;
            Debug.Log(state);
        }

        #endregion
    }
}