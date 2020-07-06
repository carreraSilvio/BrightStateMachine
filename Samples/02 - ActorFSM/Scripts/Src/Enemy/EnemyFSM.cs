namespace BrightLib.StateMachine.Samples
{
    public class EnemyFSM : ActorFSMBehaviour
    {
        private void Start()
        {
            var searchState = CreateState<SearchState>();
            var chaseState = CreateState<ChaseState>();

            var searchModule = Actor.FetchModule<SearchModule>();
            var moveModule = Actor.FetchModule<MovementModule>();

            AddTransition(searchState, chaseState, () => { return searchModule.HasTarget; });
            AddTransition(chaseState, searchState, () => { return !searchModule.HasTarget; });

            SetInitialState(searchState);
            ChangeToInitialState();
        }
    }
}