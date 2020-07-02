namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// A state that has a parent, allowing the <see cref="HFSM"/> to check for the transitions set in the parent as well.
    /// </summary>
    public class LeafState : IHFSMState
    {
        private ParentState _parentState;

        public ParentState ParentState  => _parentState;
}