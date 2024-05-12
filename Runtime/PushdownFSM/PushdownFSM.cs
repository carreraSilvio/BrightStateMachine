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

        protected Dictionary<int, List<Transition>> _overlapTransitions;
        protected Dictionary<int, List<Transition>> _quitTransitions;

        protected StateInfo _currentStateInfo;

        private readonly Stack<State> _stack;

        public PushdownFSM()
        {
            _stack = new Stack<State>();
            _overlapTransitions = new Dictionary<int, List<Transition>>();
            _quitTransitions = new Dictionary<int, List<Transition>>();

            _currentStateInfo.overlapTransitions = new List<Transition>();
            _currentStateInfo.quitTransitions = new List<Transition>();
        }

        public sealed override void Update()
        {
            if (CheckTransitions(out State state))
            {
                ChangeState(state);
            }
            else if (CheckOverlapTransitions(out State targetState))
            {
                OverlapState(targetState);
            }
            else if (CheckQuitTransitions())
            {
                QuitCurrentState();
            }
            _currentState.Update();
        }

        /// <summary>
        /// Will enter the <paramref name="toState"/> state and save the <paramref name="fromState"/> state on the stack
        /// </summary>
        public void AddOverlapTransition(State fromState, State toState, Func<bool> condition)
        {
            if (!_overlapTransitions.TryGetValue(fromState.Id, out List<Transition> currentOverlapTransition))
            {
                currentOverlapTransition = new List<Transition>();
                _overlapTransitions.Add(fromState.Id, currentOverlapTransition);
            }

            currentOverlapTransition.Add(new Transition(toState, condition));
        }

        /// <summary>
        /// Will exit the <paramref name="fromState"/> state and return the previous one on the stack
        /// </summary>
        public void AddQuitTransition(State fromState, Func<bool> condition)
        {
            if (!_quitTransitions.TryGetValue(fromState.Id, out List<Transition> currentQuitTransitions))
            {
                currentQuitTransitions = new List<Transition>();
                _quitTransitions.Add(fromState.Id, currentQuitTransitions);
            }

            currentQuitTransitions.Add(new QuitTransition(condition));
        }

        /// <summary>
        /// Put current state on the stack and enter state <paramref name="targetState"/>
        /// </summary>
        private void OverlapState(State targetState)
        {
            _stack.Push(_currentState);
            OnStateObscure?.Invoke(_currentState);
            EnterState(targetState);
            UpdateCurrentStateInfo(targetState);
        }

        /// <summary>
        /// Exit current state and return to previous one on the stack
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
            if (!_overlapTransitions.TryGetValue(state.Id, out _currentStateInfo.overlapTransitions))
            {
                _currentStateInfo.overlapTransitions = EMPTY_TRANSITIONS;
            }

            if (!_quitTransitions.TryGetValue(state.Id, out _currentStateInfo.quitTransitions))
            {
                _currentStateInfo.quitTransitions = EMPTY_TRANSITIONS;
            }
        }

    }
}