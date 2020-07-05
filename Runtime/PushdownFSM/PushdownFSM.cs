using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
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

        protected Dictionary<Type, List<Transition>> _overlapTransitions;
        protected Dictionary<Type, List<Transition>> _quitTransitions;

        protected StateInfo _currentStateInfo;

        private readonly Stack<State> _stack;

        public PushdownFSM()
        {
            _stack = new Stack<State>();
            _overlapTransitions = new Dictionary<Type, List<Transition>>();
            _quitTransitions = new Dictionary<Type, List<Transition>>();

            _currentStateInfo.overlapTransitions = new List<Transition>();
            _currentStateInfo.quitTransitions = new List<Transition>();
        }

        public sealed override void Update()
        {
            if (CheckTransitions(out State state))
            {
                ChangeState(state);
            }
            else if (CheckOverlapTransitions(out State pushState))
            {
                OverlapState(pushState);
            }
            else if (CheckQuitTransitions())
            {
                QuitCurrentState();
            }
            _currentState.Update();
        }

        /// <summary>
        /// Will enter the <paramref name="to"/> state and save the <paramref name="from"/> state in a stack you can return to
        /// </summary>
        public void AddOverlapTransition(State from, State to, Func<bool> condition)
        {
            if (!_overlapTransitions.TryGetValue(from.GetType(), out List<Transition> currentPushTransitions))
            {
                currentPushTransitions = new List<Transition>();
                _overlapTransitions.Add(from.GetType(), currentPushTransitions);
            }

            currentPushTransitions.Add(new Transition(to, condition));
        }

        /// <summary>
        /// Will exit the <paramref name="from"/> state and return the previous one on the stack
        /// </summary>
        public void AddReturnTransition(State from, Func<bool> condition)
        {
            if (!_quitTransitions.TryGetValue(from.GetType(), out List<Transition> currentPushTransitions))
            {
                currentPushTransitions = new List<Transition>();
                _quitTransitions.Add(from.GetType(), currentPushTransitions);
            }

            currentPushTransitions.Add(new QuitTransition(condition));
        }

        /// <summary>
        /// Put current state on the stack and enters state <paramref name="state"/>
        /// </summary>
        private void OverlapState(State state)
        {
            _stack.Push(_currentState);
            OnStateObscure?.Invoke(_currentState);
            EnterState(state);
            UpdateCurrentStateInfo(state);
        }

        /// <summary>
        /// Exit current state and returns to previous one on the stack
        /// </summary>
        private void QuitCurrentState()
        {
            if (_stack.Count == 0) return;

            ExitCurrentState();
            _currentState = _stack.Pop();
            OnStateFocus?.Invoke(_currentState);

            UpdateCurrentStateInfo(_currentState);
        }

        protected sealed override void ChangeState(State targetState)
        {
            ExitCurrentState();
            while (_stack.Count > 0 && _stack.Peek() != null)
            {
                _stack.Pop().Exit();
            }
            EnterState(targetState);
            UpdateCurrentStateInfo(targetState);
        }

        private bool CheckOverlapTransitions(out State result)
        {
            foreach (var transition in _currentStateInfo.overlapTransitions)
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

        private bool CheckQuitTransitions()
        {
            foreach (var transition in _currentStateInfo.quitTransitions)
            {
                if (transition.Condition())
                {
                    return true;
                }
            }

            return false;
        }

        private void UpdateCurrentStateInfo(State state)
        {
            if (!_overlapTransitions.TryGetValue(state.GetType(), out _currentStateInfo.overlapTransitions))
            {
                _currentStateInfo.overlapTransitions = _S_EMPTY_TRANSITIONS;
            }

            if (!_quitTransitions.TryGetValue(state.GetType(), out _currentStateInfo.quitTransitions))
            {
                _currentStateInfo.quitTransitions = _S_EMPTY_TRANSITIONS;
            }
        }


    }
}