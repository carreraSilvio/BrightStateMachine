namespace BrightLib.StateMachine.Samples
{
    public class EnemyFSM : ActorFSM
    {
        private void Start()
        {
            var searchState = CreateState<Search_EnemyState>();
            var chaseState = CreateState<Chase_EnemyState>();

            var searchModule = Actor.FetchModule<SearchModule>();
            var moveModule = Actor.FetchModule<MovementModule>();

            AddTransition(searchState, chaseState, () => { return searchModule.HasTarget; });
            AddTransition(chaseState, searchState, () => { return !searchModule.HasTarget; });

            SetInitialState(searchState);
            ChangeToInitialState();
        }
    }
}