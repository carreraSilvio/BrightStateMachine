﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Core FSM class
    /// </summary>
    public abstract class FSM : MonoBehaviour
    {
        //EVENTS
        public event Action<State> OnStateEnter;
        public event Action<State> OnStateExit;

        //PROPERTIES
        /// <summary>
        /// Time entered current state
        /// </summary>
        public float TimeEnteredCurrentState => _timeEnteredState;

        /// <summary>
        /// Elapsed time in current state
        /// </summary>
        public float ElapseTimeInCurrentState => Time.time - _timeEnteredState;

        /// <summary>
        /// Current state
        /// </summary>
        public State CurrentState => _currentState;

        //Private Fields
        protected State _initialState;
        protected State _currentState;
        private float _timeEnteredState;

        protected Dictionary<int, List<Transition>> _transitions = new Dictionary<int, List<Transition>>();
        protected List<Transition> _currentStateTransitions = new List<Transition>();
        protected List<Transition> _anyStateTransitions = new List<Transition>();
        protected readonly static List<Transition> EMPTY_TRANSITIONS = new List<Transition>();

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

        public void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }

        public void SetInitialState(State initialState)
        {
            _initialState = initialState;
        }

        public void ChangeToInitialState()
        {
            ChangeState(_initialState);
        }

        protected virtual void ChangeState(State targetState)
        {
            ExitCurrentState();
            EnterState(targetState);
        }

        protected virtual void EnterState(State targetState)
        {
            if (targetState == _currentState) return;

            _currentState = targetState;

            if (!_transitions.TryGetValue(_currentState.Id, out _currentStateTransitions))
            {
                _currentStateTransitions = EMPTY_TRANSITIONS;
            }

            _timeEnteredState = Time.time;
            _currentState.OnEnterInvoke();
            _currentState.Enter();
            OnStateEnter?.Invoke(_currentState);
        }

        protected virtual void ExitCurrentState()
        {
            if (_currentState == null) return;

            _currentState.OnExitInvoke();
            _currentState.Exit();
            OnStateExit?.Invoke(_currentState);
            _currentState = null;
        }

        /// <summary>
        /// Transition between <paramref name="from"/> and <paramref name="to"/> states if <paramref name="condition"/> is met
        /// </summary>
        public void AddTransition(State from, State to, Func<bool> condition)
        {
            if (!_transitions.TryGetValue(from.Id, out List<Transition> currentTransitions))
            {
                currentTransitions = new List<Transition>();
                _transitions.Add(from.Id, currentTransitions);
            }

            currentTransitions.Add(new Transition(to, condition));
        }

        public void AddAnyTransition(State to, Func<bool> condition)
        {
            _anyStateTransitions.Add(new Transition(to, condition));
        }

        protected bool CheckTransitions(out State result)
        {
            //Check parent state transition
            var state = _currentState;
            while (state.HasParentState)
            {
                state = state.ParentState;
                if (_transitions.TryGetValue(state.Id, out List<Transition> parentStateTransitions))
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

            //Check AnyState Transition
            foreach (var transition in _anyStateTransitions)
            {
                if (transition.Condition())
                {
                    result = GetLeafState(transition.Target);
                    return true;
                }
            }

            //Check current state transition
            foreach (var transition in _currentStateTransitions)
            {
                if (transition.Condition())
                {
                    result = GetLeafState(transition.Target);
                    return true;
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