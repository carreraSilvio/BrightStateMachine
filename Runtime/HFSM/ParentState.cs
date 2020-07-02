namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// State that has one initial state. Sole purpose is to reduce transition redudancy.
    /// </summary>
    public class ParentState : OrganizerState
    {
        protected IState _initialState;

        public override IState GetLeafChild()
        {
            if(_initialState is OrganizerState organizerState)
            {
                organizerState.GetLeafChild();
            }

            return _initialState;
        }
    }
}