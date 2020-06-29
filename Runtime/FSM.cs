using System;

namespace BrightLib.StateMachine.Runtime
{
    public class FSM
    {
        public State startState;

        protected State _currentState;

        public void Update()
        {
            _currentState.Update();
        }

        public void TransitionToState(State targetState)
        {
            _currentState?.Exit();
            targetState.Enter();
            _currentState = targetState;
        }

        internal void LateUpdate()
        {
            foreach(var transition in _currentState.Transitions)
            {
                if(transition.condition())
                {
                    TransitionToState(transition.target);
                    break;
                }
            }
        }
    }
}