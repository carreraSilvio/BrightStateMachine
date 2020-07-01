namespace BrightLib.StateMachine.Runtime
{
    public class NestedState : State
    {
        private NestedState _initialState;
        private NestedState _parentState;

        /// <summary>
        /// Add a parent to this state to use check for it's transitions as well
        /// </summary>
        public void SetParentState(NestedState parentState)
        {
            _parentState = parentState;
        }

        /// <summary>
        /// Set a initial state will make this state a parent that shares it's transitions
        /// </summary>
        public void SetInitialState(NestedState initialState)
        {
            _initialState = initialState;
        }

        /// <summary>
        /// A parent state will have a initial state set
        /// </summary>
        public bool IsParent => _initialState != null;

        /// <summary>
        /// A child will have a parent state set
        /// </summary>
        public bool IsChild => _parentState != null;

        public NestedState InitialState => _initialState; 
        public NestedState ParentState  => _parentState;

        /// <summary>
        /// Return the the first non-parent nested child state
        /// </summary>
        public NestedState GetLeafChild()
        {
            if(IsParent) return _initialState.GetLeafChild();

            return this;
        }

    }
}