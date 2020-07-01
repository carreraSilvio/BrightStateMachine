using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Allows the use of nested states that share transitions
    /// </summary>
    public class HFSM
    {
        protected readonly static List<Transition<NestedState>> _S_EMPTY_TRANSITIONS = new List<Transition<NestedState>>();

        public event Action<NestedState> OnStateChange;

        protected NestedState _initialState;
        protected NestedState _currentState;

        protected Dictionary<Type, List<Transition<NestedState>>> _transitions;

        protected List<Transition<NestedState>> _currentStateTransitions;
        protected List<Transition<NestedState>> _anyStateTransitions;

        public HFSM()
        {
            _transitions = new Dictionary<Type, List<Transition<NestedState>>>();
            _currentStateTransitions = new List<Transition<NestedState>>();
            _anyStateTransitions = new List<Transition<NestedState>>();
        }

        public void Update()
        {
            if (CheckTransitions(out NestedState state))
            {
                ChangeState(state);
            }
            _currentState.Update();
        }

        public void LateUpdate()
        {
            _currentState.LateUpdate();
        }

        public void ChangeToStartState() => ChangeState(_initialState);

        public void ChangeState(NestedState targetState)
        {
            if (targetState == _currentState) return;

            _currentState?.Exit();
            _currentState = targetState;

            if (!_transitions.TryGetValue(_currentState.GetType(), out _currentStateTransitions))
            {
                _currentStateTransitions = _S_EMPTY_TRANSITIONS;
            }

            _currentState.Enter();
            OnStateChange?.Invoke(_currentState);
        }

        public void AddTransition(NestedState from, NestedState to, Func<bool> condition)
        {
            if (!_transitions.TryGetValue(from.GetType(), out List<Transition<NestedState>> currentTransitions))
            {
                currentTransitions = new List<Transition<NestedState>>();
                _transitions.Add(from.GetType(), currentTransitions);
            }

            currentTransitions.Add(new Transition<NestedState>(to, condition));
        }

        public void AddAnyTransition(NestedState to, Func<bool> condition)
        {
            _anyStateTransitions.Add(new Transition<NestedState>(to, condition));
        }

        private bool CheckTransitions(out NestedState result)
        {
            foreach (var transition in _anyStateTransitions)
            {
                if (transition.Condition())
                {
                    result = transition.Target.GetLeafChild();
                    return true;
                }
            }

            foreach (var transition in _currentStateTransitions)
            {
                if (transition.Condition())
                {
                    result = transition.Target.GetLeafChild();
                    return true;
                }
            }

            var state = _currentState;
            while(state.IsChild)
            {
                state = state.ParentState;
                if (_transitions.TryGetValue(state.GetType(), out List<Transition<NestedState>> transitions))
                {
                    foreach (var transition in transitions)
                    {
                        if (transition.Condition())
                        {
                            result = transition.Target.GetLeafChild();
                            return true;
                        }
                    }
                }
            }
            

            result = default;
            return false;
        }
    }
}