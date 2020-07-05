using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    public abstract class CompositeState : HFSMState
    {
        private HFSMState _initialState;

        private Dictionary<Type, HFSMState> _children;

        public HFSMState InitialState => _initialState; 

        public CompositeState()
        {
            _children = new Dictionary<Type, HFSMState>();
        }

        public void AddChild(HFSMState state)
        {
            if (_children.ContainsKey(state.GetType())) return;

            _children.Add(state.GetType(), state);
            state.SetParent(this);
        }

        public void AddChildAsInitialState(HFSMState state)
        {
            _initialState = state;
            AddChild(state);
        }

        /// <summary>
        /// Return the first non-composite HFSM state
        /// </summary>
        public HFSMState GetLeafState()
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