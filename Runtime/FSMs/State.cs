using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Basic state for a <see cref="FSM"/>
    /// </summary>
    public class State
    {
        public int Id => _id;
        public string DisplayName { get; set; }
        public CompositeState ParentState { get; private set; }

        private static int UNIQUE_INSTANCE_ID;
        private readonly int _id = UNIQUE_INSTANCE_ID++;

        public State()
        {
            DisplayName = GetType().Name;
        }

        public State(string displayName)
        {
            DisplayName = displayName;
        }

        /// <summary>
        /// Get state name up to the the root of the FSM. 
        /// <br/>Eg: ParentStateName.SubParentStateName.StateName
        /// </summary>
        public string GetFullName()
        {
            var stack = new Stack<string>();
            var state = this;

            while (state != null)
            {
                stack.Push(state.DisplayName);
                state = state.ParentState;
            }

            return string.Join(".", stack);
        }

        internal void SetParent(CompositeState parentState)
        {
            ParentState = parentState;
        }

        public bool HasParentState()
        {
            return ParentState != null;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Resume() { }
        public virtual void Suspend() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }

        public void Log(object message)
        {
            UnityEngine.Debug.Log(message);
        }

        public override string ToString()
        {
            return $"Id {_id}\t FullName {GetFullName()}";
        }
    }
}