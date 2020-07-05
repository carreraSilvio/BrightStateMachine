using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// A FSM that allows you to stack states and keep a history
    /// </summary>
    public class PushdownFSM : FSM
    {
        private Stack<State> _stack;

        protected Dictionary<Type, List<Transition>> _pushTransitions;
        protected Dictionary<Type, List<Transition>> _popTransitions;

        protected List<Transition> _currentStatePushTransitions;
        protected List<Transition> _currentStatePopTransitions;

        public PushdownFSM()
        {
            _stack = new Stack<State>();
            _pushTransitions = new Dictionary<Type, List<Transition>>();
            _popTransitions = new Dictionary<Type, List<Transition>>();

            _currentStatePushTransitions = new List<Transition>();
            _currentStatePopTransitions = new List<Transition>();
        }

        public override void Update()
        {
            if (CheckTransitions(out State state))
            {
                ChangeState(state);
            }
            if (CheckPushTransitions(out State pushState))
            {
                PushState(pushState);
            }
            if (CheckPopTransitions())
            {
                PopState();
            }
            _currentState.Update();
        }

        private void PushState(State state)
        {
            _stack.Push(_currentState);
            //_currentState = state;
            //_currentState.Enter();
            EnterState(state);

            if (!_transitions.TryGetValue(_currentState.GetType(), out _currentStateTransitions))
            {
                _currentStateTransitions = _S_EMPTY_TRANSITIONS;
            }

            if (!_pushTransitions.TryGetValue(_currentState.GetType(), out _currentStatePushTransitions))
            {
                _currentStatePushTransitions = _S_EMPTY_TRANSITIONS;
            }

            if (!_popTransitions.TryGetValue(_currentState.GetType(), out _currentStatePopTransitions))
            {
                _currentStatePopTransitions = _S_EMPTY_TRANSITIONS;
            }
        }

        private void PopState()
        {
            _currentState.Exit();
            _currentState = _stack.Pop();
        }

        protected sealed override void ChangeState(State targetState)
        {
            while (_stack.Count > 0 && _stack.Peek() != null)
            {
                _stack.Pop().Exit();
            }
            base.ChangeState(targetState);

            if (!_pushTransitions.TryGetValue(_currentState.GetType(), out _currentStatePushTransitions))
            {
                _currentStatePushTransitions = _S_EMPTY_TRANSITIONS;
            }

            if (!_popTransitions.TryGetValue(_currentState.GetType(), out _currentStatePopTransitions))
            {
                _currentStatePopTransitions = _S_EMPTY_TRANSITIONS;
            }
        }

        /// <summary>
        /// When in state <paramref name="from"/>, if <paramref name="condition"/> is true then enter state <paramref name="to"/>. Current state will be put on a stack.
        /// </summary>
        public void AddPushTransition(State from, State to, Func<bool> condition)
        {
            if (!_pushTransitions.TryGetValue(from.GetType(), out List<Transition> currentPushTransitions))
            {
                currentPushTransitions = new List<Transition>();
                _pushTransitions.Add(from.GetType(), currentPushTransitions);
            }

            currentPushTransitions.Add(new Transition(to, condition));
        }

        /// <summary>
        /// When in state <paramref name="from"/>, if <paramref name="condition"/> is true then exit <paramref name="from"/> state and return to previous state on the stack.
        /// </summary>
        public void AddPopTransition(State from, Func<bool> condition)
        {
            if (!_pushTransitions.TryGetValue(from.GetType(), out List<Transition> currentPushTransitions))
            {
                currentPushTransitions = new List<Transition>();
                _pushTransitions.Add(from.GetType(), currentPushTransitions);
            }

            currentPushTransitions.Add(new PopTransition(condition));
        }

        protected bool CheckPushTransitions(out State result)
        {
            foreach (var transition in _anyStateTransitions)
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

        protected bool CheckPopTransitions()
        {
            foreach (var transition in _anyStateTransitions)
            {
                if (transition.Condition())
                {
                    return true;
                }
            }

            return false;
        }


    }
}