using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    public abstract class CompositeState : State
    {
        private State _initialState;

        private Dictionary<Type, State> _children;

        public State InitialState => _initialState; 

        public CompositeState()
        {
            _children = new Dictionary<Type, State>();
        }

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
        /// Return the first non-composite HFSM state
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
            return;
        }

        public override void Update()
        {
            return;
        }

        public override void LateUpdate()
        {
            return;
        }
        public override void Exit()
        {
            return;
        }
    }
}