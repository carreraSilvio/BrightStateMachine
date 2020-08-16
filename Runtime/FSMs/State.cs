using System;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Basic state for <see cref="FSM"/>
    /// </summary>
    public class State
    {
        private static int _S_UNIQUE_INSTANCE_ID;

        private readonly int _id = _S_UNIQUE_INSTANCE_ID++;

        private CompositeState _parentState;

        protected string _displayName;

        public CompositeState ParentState => _parentState;
        public bool HasParentState => _parentState != null;
        public int Id => _id;
        public string DisplayName => _displayName;

        public event Action<State> OnEnter;
        public event Action<State> OnExit;

        public State()
        {
            _displayName = GetType().Name;
        }

        public State(string displayName)
        {
            _displayName = displayName;
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
            _parentState = parentState;
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