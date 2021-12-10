using System;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Basic state for <see cref="FSM"/>
    /// </summary>
    public class State
    {
        private static int UNIQUE_INSTANCE_ID;

        private readonly int _id = UNIQUE_INSTANCE_ID++;

        public CompositeState ParentState { get; private set; }
        public bool HasParentState => ParentState != null;
        public int Id => _id;
        public string DisplayName { get; set; }

        public event Action<State> OnEnter;
        public event Action<State> OnExit;

        public State()
        {
            DisplayName = GetType().Name;
        }

        public State(string displayName)
        {
            DisplayName = displayName;
        }

        /// <summary>
        /// Return state name up to the the root of the FSM. 
        /// Eg: ParentStateName.SubParentStateName.StateName
        /// </summary>
        public string FullName()
        {
            var fullName = "";
            var state = this;
            while (state.HasParentState)
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

        public void Log(object message) => UnityEngine.Debug.Log(message);

        public override string ToString() => $"Id {_id}\t FullName {FullName()}";

        internal virtual void OnEnterInvoke() => OnEnter?.Invoke(this);
        internal virtual void OnExitInvoke() => OnExit?.Invoke(this);
    }
}