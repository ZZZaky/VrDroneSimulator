using System.Collections.Generic;
using UnityEngine;

namespace FDS.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private StateType typeOfControlState;

        [SerializeField]
        protected State defaultOutput;

        [SerializeField]
        private List<StateTransition> transitions;

        private Dictionary<string, State> transitionsDict;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            transitionsDict = new Dictionary<string, State>();
            foreach (var transition in transitions)
                transitionsDict.Add(transition.TriggerName, transition.State);
        }

        #endregion

        #region Public Methods

        public enum StateType
        {
            Character,
            Drone
        }

        public State GetNextState(string transition, bool useDefault = true)
        {
            if(transitionsDict.ContainsKey(transition))
                return transitionsDict[transition];
            if(useDefault)
                return GetDefaultNextState();
            return null;
        }

        public State GetDefaultNextState()
        {
            return defaultOutput;
        }

        public virtual void OnEnter() { }

        public virtual void OnExit() { }

        public virtual void Execute() { }

        #endregion

        #region Protected Methods

        public StateType GetTypeOfControlState()
        {
            return typeOfControlState;
        }

        #endregion
    }
}