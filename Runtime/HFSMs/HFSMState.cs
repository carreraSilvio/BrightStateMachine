namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Basic state for <see cref="HFSM"/>
    /// </summary>
    public abstract class HFSMState
    {
        protected CompositeState _parentState;

        public bool HasParentState => _parentState != null;
        public CompositeState ParentState => _parentState;

        public void Log(object message) => UnityEngine.Debug.Log(message);

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

        public virtual void Exit()
        {

        }

        /// <summary>
        /// Returns a State name up to the root HFSMState. Eg: ParentStateName.SubParentStateName.StateName
        /// </summary>
        public string FullName()
        {
            var fullName = "";
            var state = this;
            while (state.HasParentState)
            {
                fullName += state.ParentState.GetType().Name  + ".";
                state = state.ParentState;
            }
            fullName += GetType().Name;
            return fullName;
        }
    }
}