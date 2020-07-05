using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    public struct StateInfo
    {
        public List<Transition> PushTransitions { get; set; }
        public List<Transition> PopTransitions { get; set; }
    }


    /// <summary>
    /// A FSM that allows you to stack states to return to
    /// </summary>
    public class PushdownFSM : FSM
    {
        /// <summary>
        /// Called when your return to a state that was overlapped
        /// </summary>
        public event Action<State> OnStateFocus;

        /// <summary>
        /// Called when a state is overlapped by another
        /// </summary>
        public event Action<State> OnStateObscure;

        protected Dictionary<Type, List<Transition>> _pushTransitions;
        protected Dictionary<Type, List<Transition>> _popTransitions;

        protected List<Transition> _currentStatePushTransitions;
        protected List<Transition> _currentStatePopTransitions;

        private readonly Stack<State> _stack;

        public PushdownFSM()
        {
            _stack = new Stack<State>();
            _pushTransitions = new Dictionary<Type, List<Transition>>();
            _popTransitions = new Dictionary<Type, List<Transition>>();

            _currentStatePushTransitions = new List<Transition>();
            _currentStatePopTransitions = new List<Transition>();
        }

        public sealed override void Update()
        {
            if (CheckTransitions(out State state))
            {
                ChangeState(state);
            }
            else if (CheckPushTransitions(out State pushState))
            {
                OverlapState(pushState);
            }
            else if (CheckPopTransitions())
            {
                ReturnToPreviousState();
            }
            _currentState.Update();
        }

        /// <summary>
        /// Will enter the <paramref name="to"/> state and save the <paramref name="from"/> state in a stack you can return to
        /// </summary>
        public void AddOverlapTransition(State from, State to, Func<bool> condition)
        {
            if (!_pushTransitions.TryGetValue(from.GetType(), out List<Transition> currentPushTransitions))
            {
                currentPushTransitions = new List<Transition>();
                _pushTransitions.Add(from.GetType(), currentPushTransitions);
            }

            currentPushTransitions.Add(new Transition(to, condition));
        }

        /// <summary>
        /// Will exit the <paramref name="from"/> state and return the previous one on the stack
        /// </summary>
        public void AddReturnTransition(State from, Func<bool> condition)
        {
            if (!_popTransitions.TryGetValue(from.GetType(), out List<Transition> currentPushTransitions))
            {
                currentPushTransitions = new List<Transition>();
                _popTransitions.Add(from.GetType(), currentPushTransitions);
            }

            currentPushTransitions.Add(new PopTransition(condition));
        }

        private void OverlapState(State state)
        {
            _stack.Push(_currentState);
            OnStateObscure?.Invoke(_currentState);
            EnterState(state);

            if (!_pushTransitions.TryGetValue(_currentState.GetType(), out _currentStatePushTransitions))
            {
                _currentStatePushTransitions = _S_EMPTY_TRANSITIONS;
            }

            if (!_popTransitions.TryGetValue(_currentState.GetType(), out _currentStatePopTransitions))
            {
                _currentStatePopTransitions = _S_EMPTY_TRANSITIONS;
            }
        }

        private void ReturnToPreviousState()
        {
            if (_stack.Count == 0) return;

            ExitCurrentState();
            _currentState = _stack.Pop();
            OnStateFocus?.Invoke(_currentState);

            if (!_pushTransitions.TryGetValue(_currentState.GetType(), out _currentStatePushTransitions))
            {
                _currentStatePushTransitions = _S_EMPTY_TRANSITIONS;
            }

            if (!_popTransitions.TryGetValue(_currentState.GetType(), out _currentStatePopTransitions))
            {
                _currentStatePopTransitions = _S_EMPTY_TRANSITIONS;
            }
        }

        protected sealed override void ChangeState(State targetState)
        {
            ExitCurrentState();
            while (_stack.Count > 0 && _stack.Peek() != null)
            {
                _stack.Pop().Exit();
            }
            EnterState(targetState);

            if (!_pushTransitions.TryGetValue(_currentState.GetType(), out _currentStatePushTransitions))
            {
                _currentStatePushTransitions = _S_EMPTY_TRANSITIONS;
            }

            if (!_popTransitions.TryGetValue(_currentState.GetType(), out _currentStatePopTransitions))
            {
                _currentStatePopTransitions = _S_EMPTY_TRANSITIONS;
            }
        }

        private bool CheckPushTransitions(out State result)
        {
            foreach (var transition in _currentStatePushTransitions)
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

        private bool CheckPopTransitions()
        {
            foreach (var transition in _currentStatePopTransitions)
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