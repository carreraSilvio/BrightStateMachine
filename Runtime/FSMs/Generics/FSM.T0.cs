using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public class FSM<T> where T : Component
    {
        private readonly static List<Transition<T>> _S_EMPTY_TRANSITIONS = new List<Transition<T>>();

        protected T _component;

        protected State<T> _startState;
        private State<T> _currentState;

        private Dictionary<Type, List<Transition<T>> > _transitions;

        private List<Transition<T>> _currentStateTransitions;
        private List<Transition<T>> _anyStateTransitions;

        public T Component => _component;
        public GameObject GameObject => _component.gameObject;

        public FSM(T component)
        {
            _transitions = new Dictionary<Type, List<Transition<T>>>();
            _currentStateTransitions = new List<Transition<T>>();
            _anyStateTransitions = new List<Transition<T>>();

            _component = component;
        }

        public void Update()
        {
            if (CheckTransitions(out Transition<T> transition))
            {
                ChangeState(transition.Target);
            }
            _currentState.Update();
        }

        internal void LateUpdate()
        {
            _currentState.LateUpdate();
        }

        public void ChangeToStartState() => ChangeState(_startState);

        public void ChangeState(State<T> targetState)
        {
            if (targetState == _currentState) return;

            _currentState?.Exit();
            _currentState = targetState;

            if (!_transitions.TryGetValue(_currentState.GetType(), out _currentStateTransitions))
            {
                _currentStateTransitions = _S_EMPTY_TRANSITIONS;
            }

            _currentState.Enter();
        }

        public void AddTransition(State<T> from, State<T> to, Func<bool> condition)
        {
            if (!_transitions.TryGetValue(from.GetType(), out List<Transition<T>> currentTransitions))
            {
                currentTransitions = new List<Transition<T>>();
                _transitions.Add(from.GetType(), currentTransitions);
            }

            currentTransitions.Add(new Transition<T>(to, condition));
        }

        public void AddAnyTransition(State<T> to, Func<bool> condition)
        {
            _anyStateTransitions.Add(new Transition<T>(to, condition));
        }

        private bool CheckTransitions(out Transition<T> result)
        {
            foreach (var transition in _anyStateTransitions)
            {
                if (transition.Condition())
                {
                    result = transition;
                    return true;
                }
            }

            foreach (var transition in _currentStateTransitions)
            {
                if (transition.Condition())
                {
                    result = transition;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public T1 CreateState<T1>() where T1 : State<T>
        {
            return (T1) System.Activator.CreateInstance(typeof(T1), new object[] { _component });
        }

    }
}