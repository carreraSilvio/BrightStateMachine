using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Core FSM class
    /// </summary>
    public class FSM : MonoBehaviour
    {
        /// <summary>
        /// Lower order FSMs are run first in a <see cref="LayeredFSM"/>.
        /// </summary>
        [SerializeField]
        private int _priority = 0;

        /// <summary>
        /// Descriptive name for the FSM.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// If true, it will log the state transitions.
        /// </summary>
        public bool LogTransitions { get; set; }
        /// <summary>
        /// If true, it will run Tick, LateTick and FixedTick. Otherwise it will skip it.
        /// </summary>
        public bool Running { get; set; } = true;
        /// <summary>
        /// If true, it will update itseful. Used when part of a LayeredFSM.
        /// </summary>
        internal bool AutoUpdate { get; set; } = true;

        /// <summary>
        /// Invoked when a state is entered.
        /// </summary>
        public event Action<State> OnStateEnter;

        /// <summary>
        /// Invoked when a state is exited.
        /// </summary>
        public event Action<State> OnStateExit;

        /// <summary>
        /// Time entered current state
        /// </summary>
        public float TimeEnteredCurrentState => _timeEnteredState;

        /// <summary>
        /// Time elapsed since it entered current state
        /// </summary>
        public float TimeElapsedInCurrentState => Time.time - _timeEnteredState;

        /// <summary>
        /// Higher priority FSMs are run first in a <see cref="LayeredFSM"/>.
        /// </summary>
        public int Priority => _priority;

        protected State _initialState;
        protected State _currentState;
        private string _initialStateDisplayName;
        private string _currentStateDisplayName;
        private float _timeEnteredState;

        protected Dictionary<Type, State> _states = new Dictionary<Type, State>();

        protected Dictionary<int, List<Transition>> _transitions = new Dictionary<int, List<Transition>>();
        protected List<Transition> _currentStateTransitions = new List<Transition>();
        protected List<Transition> _anyStateTransitions = new List<Transition>();
        protected readonly static List<Transition> EMPTY_TRANSITIONS = new List<Transition>();
        private FSMEventManager _eventManager;

        private void Awake()
        {
            DisplayName = "FSM";
        }

        public void Update()
        {
            if(!AutoUpdate)
            {
                return;
            }
            Tick();
        }

        public void LateUpdate()
        {
            if (!AutoUpdate)
            {
                return;
            }
            LateTick();
        }

        public void FixedUpdate()
        {
            if (!AutoUpdate)
            {
                return;
            }
            FixedTick();
        }

        internal virtual void Tick()
        {
            if(!Running)
            {
                return;
            }
            if (CheckTransitions(out State state))
            {
                ChangeState(state);
            }
            _currentState.Tick();
        }

        internal void LateTick()
        {
            if (!Running)
            {
                return;
            }
            _currentState.LateTick();
        }

        internal void FixedTick()
        {
            if (!Running)
            {
                return;
            }
            _currentState.FixedTick();
        }

        internal void InjectEventManager(FSMEventManager manager)
        {
            _eventManager = manager;

            // Inject into existing states
            foreach (var state in _states.Values)
            {
                state.InjectEventManager(manager);
            }
        }

        public void AddState(State state)
        {
            if(_states.ContainsKey(state.GetType()))
            {
                return;
            }
            _states.Add(state.GetType(), state);

            if (_eventManager != null)
            {
                state.InjectEventManager(_eventManager);
            }
        }

        public void SetInitialState(State initialState)
        {
            _initialState = initialState;
            _initialStateDisplayName = _initialState.DisplayName;
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
            if (targetState == _currentState)
            {
                return;
            }

            _currentState = targetState;
            _currentStateDisplayName = _currentState.DisplayName;

            if (!_transitions.TryGetValue(_currentState.Id, out _currentStateTransitions))
            {
                _currentStateTransitions = EMPTY_TRANSITIONS;
            }

            Log($"Enter State \t{_currentState.GetFullName()}");
            _timeEnteredState = Time.time;
            _currentState.Enter();
            OnStateEnter?.Invoke(_currentState);
        }

        protected virtual void ExitCurrentState()
        {
            if (_currentState == null)
            {
                return;
            }

            Log($"Exit State \t{_currentState.GetFullName()}");
            _currentState.Exit();
            OnStateExit?.Invoke(_currentState);
            _currentState = null;
            _currentStateDisplayName = "null";
        }

        /// <summary>
        /// Adds a transition between <paramref name="fromState"/> and 
        /// <paramref name="toState"/> states if <paramref name="condition"/> is met
        /// </summary>
        public void AddTransition(State fromState, State toState, Func<bool> condition)
        {
            AddState(fromState);
            AddState(toState);
            if (!_transitions.TryGetValue(fromState.Id, out List<Transition> currentTransitions))
            {
                currentTransitions = new List<Transition>();
                _transitions.Add(fromState.Id, currentTransitions);
            }

            currentTransitions.Add(new Transition(toState, condition));
        }

        public void AddAnyTransition(State to, Func<bool> condition)
        {
            _anyStateTransitions.Add(new Transition(to, condition));
        }

        protected bool CheckTransitions(out State result)
        {
            //Check parent state transition
            var state = _currentState;
            while (state.HasParentState())
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

        protected void Log(object message)
        {
            if(LogTransitions)
            {
                Debug.Log($"{DisplayName}: {message}");
            }
        }


    }

}