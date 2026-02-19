using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// A stack-based FSM 
    /// </summary>
    /// <remarks>
    /// Supports temporary state interruptions by pushing the current state
    /// onto a stack and resuming it later. Recommended for Menu/Game state transitions.
    /// </remarks>
    public class PushdownFSM : FSM
    {
        /// <summary>
        /// Invoked when a previously suspended state regains control.
        /// </summary>
        public event Action<State> OnStateResume;

        /// <summary>
        /// Invoked when the current state is suspended due to a push.
        /// </summary>
        public event Action<State> OnStateSuspend;

        protected Dictionary<int, List<Transition>> _overlapTransitions;
        protected Dictionary<int, List<Transition>> _quitTransitions;

        protected StateInfo _currentStateInfo;

        private readonly Stack<State> _stack;

        public PushdownFSM()
        {
            _stack = new Stack<State>();
            _overlapTransitions = new Dictionary<int, List<Transition>>();
            _quitTransitions = new Dictionary<int, List<Transition>>();

            _currentStateInfo.pushTransitions = new List<Transition>();
            _currentStateInfo.popTransitions = new List<Transition>();
        }

        public sealed override void Update()
        {
            if (CheckTransitions(out State state))
            {
                ChangeState(state);
            }
            else if (CheckPushTransitions(out State targetState))
            {
                PushState(targetState);
            }
            else if (CheckPopTransitions())
            {
                PopCurrentState();
            }
            _currentState.Update();
        }

        /// <summary>
        /// Pushes <paramref name="fromState"/> onto the stack and enters <paramref name="toState"/>
        /// when the specified <paramref name="condition"/> evaluates to true.
        /// </summary>
        public void AddPushTransition(State fromState, State toState, Func<bool> condition)
        {
            if (!_overlapTransitions.TryGetValue(fromState.Id, out List<Transition> currentOverlapTransition))
            {
                currentOverlapTransition = new List<Transition>();
                _overlapTransitions.Add(fromState.Id, currentOverlapTransition);
            }

            currentOverlapTransition.Add(new Transition(toState, condition));
        }

        /// <summary>
        /// Pops the current state from the stack and resumes the previous state
        /// when the specified <paramref name="condition"/> evaluates to true.
        /// </summary>
        public void AddPopTransition(State fromState, Func<bool> condition)
        {
            if (!_quitTransitions.TryGetValue(fromState.Id, out List<Transition> currentQuitTransitions))
            {
                currentQuitTransitions = new List<Transition>();
                _quitTransitions.Add(fromState.Id, currentQuitTransitions);
            }

            currentQuitTransitions.Add(new QuitTransition(condition));
        }

        /// <summary>
        /// Suspends the current state by pushing it onto the stack,
        /// then enters <paramref name="targetState"/>.
        /// </summary>
        private void PushState(State targetState)
        {
            _stack.Push(_currentState);
            OnStateSuspend?.Invoke(_currentState);
            EnterState(targetState);
            UpdateCurrentStateInfo(targetState);
        }

        /// <summary>
        /// Exits the current state and resumes the previous state from the stack.
        /// </summary>
        private void PopCurrentState()
        {
            if (_stack.Count == 0)
            {
                return;
            }

            ExitCurrentState();
            _currentState = _stack.Pop();
            OnStateResume?.Invoke(_currentState);

            UpdateCurrentStateInfo(_currentState);
        }

        /// <summary>
        /// Performs a hard transition:
        /// <br/> - Exits the current state
        /// <br/> - Clears the entire stack
        /// <br/> - Enters <paramref name="targetState"/>
        /// </summary>
        protected sealed override void ChangeState(State targetState)
        {
            ExitCurrentState();
            while (_stack.Count > 0)
            {
                _stack.Pop().Exit();
            }
            EnterState(targetState);
            UpdateCurrentStateInfo(targetState);
        }

        private bool CheckPushTransitions(out State result)
        {
            foreach (var transition in _currentStateInfo.pushTransitions)
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
            foreach (var transition in _currentStateInfo.popTransitions)
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
            if (!_overlapTransitions.TryGetValue(state.Id, out _currentStateInfo.pushTransitions))
            {
                _currentStateInfo.pushTransitions = EMPTY_TRANSITIONS;
            }

            if (!_quitTransitions.TryGetValue(state.Id, out _currentStateInfo.popTransitions))
            {
                _currentStateInfo.popTransitions = EMPTY_TRANSITIONS;
            }
        }

    }
}