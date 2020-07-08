using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// State that acts as parent to other states
    /// </summary>
    public abstract class CompositeState : State
    {
        private State _initialState;

        private readonly Dictionary<Type, State> _children;

        public State InitialState => _initialState; 

        public CompositeState()
        {
            _children = new Dictionary<Type, State>();
        }

        /// <summary>
        /// Add a state as child so it has access to the transitions set to the composite state
        /// </summary>
        public void AddChild(State state)
        {
            if (_children.ContainsKey(state.GetType())) return;

            _children.Add(state.GetType(), state);
            state.SetParent(this);
        }

        public void AddChildAsInitialState(State state)
        {
            _initialState = state;
            AddChild(state);
        }

        /// <summary>
        /// Return the first non-composite state
        /// </summary>
        public State GetLeafState()
        {
            if(_initialState is CompositeState compositeState)
            {
                compositeState.GetLeafState();
            }

            return _initialState;
        }

        public override void Enter()
        {
           //NA
        }

        public override void Update()
        {
            //NA
        }

        public override void LateUpdate()
        {
            //NA
        }
        public override void Exit()
        {
            //NA;
        }
    }
}