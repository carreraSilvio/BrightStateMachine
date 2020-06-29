using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    public class GenericFSM<T> where T : GenericState
    {
        public T startState;

        protected T _currentState;

        protected Dictionary<Type, List<GenericTransition<T>>> _transitions;

        private List<GenericTransition<T>> _currentStateTransitions;

        public void Update()
        {
            _currentState.Update();
        }

        internal void LateUpdate()
        {
            foreach (var transition in _currentStateTransitions)
            {
                if (transition.Condition())
                {
                    TransitionToState(transition.Target);
                    break;
                }
            }
        }

        public void TransitionToState(T targetState)
        {
            _currentState?.Exit();
            targetState.Enter();
            _currentState = targetState;
            _currentStateTransitions = _transitions[_currentState.GetType()];
        }

        public void AddTransition(T from, T to, Func<bool> condition)
        {
            var currentTransitions = _transitions[from.GetType()];
            currentTransitions.Add(new GenericTransition<T>(to, condition));
        }
    }
}