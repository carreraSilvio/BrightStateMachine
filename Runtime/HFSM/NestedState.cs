namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// State that has a parent and a child
    /// </summary>
    public class NestedState : ParentState
    {
        private ParentState _parentState;

        protected ParentState ParentState => _parentState;
    }
}