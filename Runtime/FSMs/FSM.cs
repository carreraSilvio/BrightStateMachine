﻿using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    public class FSM
    {
        protected State _startState;

        protected State _currentState;

        protected Dictionary<Type, List<Transition>> _transitions;

        private List<Transition> _currentStateTransitions;
        private List<Transition> _anyStateTransitions;

        private readonly static List<Transition> _S_EMPTY_TRANSITIONS = new List<Transition>();

        public FSM()
        {
            _transitions = new Dictionary<Type, List<Transition>>();
            _currentStateTransitions = new List<Transition>();
            _anyStateTransitions = new List<Transition>();
        }

        public void Update()
        {
            if(CheckTransitions(out Transition transition))
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

        public void ChangeState(State targetState)
        {
            if (targetState == _currentState) return;

            _currentState?.Exit();
            _currentState = targetState;

            if(!_transitions.TryGetValue(_currentState.GetType(), out _currentStateTransitions))
            {
                _currentStateTransitions = _S_EMPTY_TRANSITIONS;
            }

            _currentState.Enter();
        }

        public void AddTransition(State from, State to, Func<bool> condition)
        {
            if(!_transitions.TryGetValue(from.GetType(), out List<Transition> currentTransitions))
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

        private bool CheckTransitions(out Transition result)
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
    }
}