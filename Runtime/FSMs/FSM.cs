﻿using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Core FSM class
    /// </summary>
    public class FSM
    {
        protected readonly static List<Transition> _S_EMPTY_TRANSITIONS = new List<Transition>();

        public event Action<State> OnStateEnter;
        public event Action<State> OnStateExit;

        protected State _initialState;
        protected State _currentState;

        protected Dictionary<Type, List<Transition>> _transitions;

        protected List<Transition> _currentStateTransitions;
        protected List<Transition> _anyStateTransitions;

        public FSM()
        {
            _transitions = new Dictionary<Type, List<Transition>>();
            _currentStateTransitions = new List<Transition>();
            _anyStateTransitions = new List<Transition>();
        }

        public virtual void Update()
        {
            if (CheckTransitions(out State state))
            {
                ChangeState(state);
            }
            _currentState.Update();
        }

        public void LateUpdate()
        {
            _currentState.LateUpdate();
        }

        public void ChangeStateToInitialState() => ChangeState(_initialState);

        protected virtual void ChangeState(State targetState)
        {
            ExitCurrentState();
            EnterState(targetState);
        }

        protected virtual void EnterState(State targetState)
        {
            if (targetState == _currentState) return;

            _currentState = targetState;

            if (!_transitions.TryGetValue(_currentState.GetType(), out _currentStateTransitions))
            {
                _currentStateTransitions = _S_EMPTY_TRANSITIONS;
            }

            _currentState.Enter();
            OnStateEnter?.Invoke(_currentState);
        }

        protected virtual void ExitCurrentState()
        {
            if (_currentState == null) return;

            OnStateExit?.Invoke(_currentState);
            _currentState = null;
        }

        public void AddTransition(State from, State to, Func<bool> condition)
        {
            if (!_transitions.TryGetValue(from.GetType(), out List<Transition> currentTransitions))
            {
                currentTransitions = new List<Transition>();
                _transitions.Add(from.GetType(), currentTransitions);
            }

            currentTransitions.Add(new Transition(to, condition));
        }

        public void AddAnyTransition(State to, Func<bool> condition)
        {
            _anyStateTransitions.Add(new Transition(to, condition));
        }

        protected bool CheckTransitions(out State result)
        {
            foreach (var transition in _anyStateTransitions)
            {
                if (transition.Condition())
                {
                    result = GetLeafState(transition.Target);
                    return true;
                }
            }

            foreach (var transition in _currentStateTransitions)
            {
                if (transition.Condition())
                {
                    result = GetLeafState(transition.Target);
                    return true;
                }
            }

            var state = _currentState;
            while(state.HasParentState)
            {
                state = state.ParentState;
                if (_transitions.TryGetValue(state.GetType(), out List<Transition> parentStateTransitions))
                {
                    foreach (var transition in parentStateTransitions)
                    {
                        if (transition.Condition())
                        {
                            result = GetLeafState(transition.Target);
                            return true;
                        }
                    }
                }
            }

            result = default;
            return false;
        }

        protected State GetLeafState(State state)
        {
            if (state is CompositeState compositeState)
            {
                return compositeState.GetLeafState();
            }
            return state;
        }
    }

}