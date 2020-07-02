using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Allows the use of nested states that share transitions
    /// </summary>
    public class HFSM
    {
        protected readonly static List<Transition<IState>> _S_EMPTY_TRANSITIONS = new List<Transition<IState>>();

        public event Action<IState> OnStateChange;

        protected IState _initialState;
        protected IState _currentState;

        protected Dictionary<Type, List<Transition<IState>>> _transitions;

        protected List<Transition<IState>> _currentStateTransitions;
        protected List<Transition<IState>> _anyStateTransitions;

        public HFSM()
        {
            _transitions = new Dictionary<Type, List<Transition<IState>>>();
            _currentStateTransitions = new List<Transition<IState>>();
            _anyStateTransitions = new List<Transition<IState>>();
        }

        public void Update()
        {
            if (CheckTransitions(out IState state))
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

        public void ChangeState(IState targetState)
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

        public void AddTransition(IState from, IState to, Func<bool> condition)
        {
            if (!_transitions.TryGetValue(from.GetType(), out List<Transition<IState>> currentTransitions))
            {
                currentTransitions = new List<Transition<IState>>();
                _transitions.Add(from.GetType(), currentTransitions);
            }

            currentTransitions.Add(new Transition<IState>(to, condition));
        }

        public void AddAnyTransition(NestedState to, Func<bool> condition)
        {
            _anyStateTransitions.Add(new Transition<IState>(to, condition));
        }

        private bool CheckTransitions(out IState result)
        {
            foreach (var transition in _anyStateTransitions)
            {
                if (transition.Condition())
                {
                    result = transition.Target is OrganizerState ? transition.Target.GetLeafChild() : ;
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
                if (_transitions.TryGetValue(state.GetType(), out List<Transition<IState>> transitions))
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