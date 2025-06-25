using System;

namespace FDS.StateMachine
{
    [Serializable]
    public struct StateTransition
    {
        public State State;
        public string TriggerName;
    }
}