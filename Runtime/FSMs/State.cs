using System;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Basic state for <see cref="FSM"/>
    /// </summary>
    public abstract class State
    {
        private static int _S_UNIQUE_INSTANCE_ID;

        private readonly int _id = _S_UNIQUE_INSTANCE_ID++;

        private CompositeState _parentState;

        public CompositeState ParentState => _parentState;
        public bool HasParentState => _parentState != null;
        public int Id => _id;

        /// <summary>
        /// Optional friendly name
        /// </summary>
        public string DisplayName { get; set; }

        public event Action<State> OnEnter;
        public event Action<State> OnExit;

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
                fullName += state.ParentState.GetType().Name + ".";
                state = state.ParentState;
            }
            fullName += GetType().Name;
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

        public override string ToString()
        {
            return $"FullName\t {FullName()}\t Id\t {_id}";
        }

        internal virtual void OnEnterInvoke() => OnEnter?.Invoke(this);
        internal virtual void OnExitInvoke() => OnExit?.Invoke(this);
    }
}