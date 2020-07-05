using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Allows the use of nested states that share transitions
    /// </summary>
    public class HFSM
    {
        protected readonly static List<HFSMTransition> _S_EMPTY_TRANSITIONS = new List<HFSMTransition>();

        public event Action<HFSMState> OnStateEnter;
        public event Action<HFSMState> OnStateExit;

        protected HFSMState _initialState;
        protected HFSMState _currentState;

        protected Dictionary<Type, List<HFSMTransition>> _transitions;

        protected List<HFSMTransition> _currentStateTransitions;
        protected List<HFSMTransition> _anyStateTransitions;

        public HFSM()
        {
            _transitions = new Dictionary<Type, List<HFSMTransition>>();
            _currentStateTransitions = new List<HFSMTransition>();
            _anyStateTransitions = new List<HFSMTransition>();
        }

        public void Update()
        {
            if (CheckTransitions(out HFSMState state))
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

        public void ChangeState(HFSMState targetState)
        {
            if (targetState == _currentState) return;

            var previousState = _currentState;
            _currentState?.Exit();
            _currentState = targetState;

            if (!_transitions.TryGetValue(_currentState.GetType(), out _currentStateTransitions))
            {
                _currentStateTransitions = _S_EMPTY_TRANSITIONS;
            }

            _currentState.Enter();
            if (previousState != null) OnStateExit?.Invoke(previousState);
            OnStateEnter?.Invoke(_currentState);
        }

        public void AddTransition(HFSMState from, HFSMState to, Func<bool> condition)
        {
            if (!_transitions.TryGetValue(from.GetType(), out List<HFSMTransition> currentTransitions))
            {
                currentTransitions = new List<HFSMTransition>();
                _transitions.Add(from.GetType(), currentTransitions);
            }

            currentTransitions.Add(new HFSMTransition(to, condition));
        }

        public void AddAnyTransition(HFSMState to, Func<bool> condition)
        {
            _anyStateTransitions.Add(new HFSMTransition(to, condition));
        }

        private bool CheckTransitions(out HFSMState result)
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
                if (_transitions.TryGetValue(state.GetType(), out List<HFSMTransition> parentStateTransitions))
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

        private HFSMState GetLeafState(HFSMState state)
        {
            if (state is CompositeState compositeState)
            {
                return compositeState.GetLeafState();
            }
            return state;
        }
    }

}