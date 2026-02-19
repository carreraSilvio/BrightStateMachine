using System;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Basic state for <see cref="FSM"/>
    /// </summary>
    [System.Serializable]
    public class State
    {
        public int Id => _id;
        public string DisplayName { get; set; }
        public CompositeState ParentState { get; private set; }

        internal event Action<State> OnEnter;
        internal event Action<State> OnExit;

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
            var fullName = "";
            var state = this;
            while (state.GetHasParentState())
            {
                fullName += state.ParentState.DisplayName + ".";
                state = state.ParentState;
            }
            fullName += DisplayName;
            return fullName;
        }

        internal void SetParent(CompositeState parentState)
        {
            ParentState = parentState;
        }

        public bool GetHasParentState()
        {
            return ParentState != null;
        }

        public virtual void Enter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void LateUpdate()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Exit()
        {
        }

        public void Log(object message)
        {
            UnityEngine.Debug.Log(message);
        }

        public override string ToString()
        {
            return $"Id {_id}\t FullName {GetFullName()}";
        }

        internal virtual void OnEnterInvoke() => OnEnter?.Invoke(this);
        internal virtual void OnExitInvoke() => OnExit?.Invoke(this);
    }
}