using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public class EnemyFSM : HFSM<Actor>
    {
        public EnemyFSM(Actor actor) : base(actor)
        {
            var searchState = CreateState<SearchState>();
            var chaseState = CreateState<ChaseState>();

            var searchModule = actor.FetchModule<SearchModule>();
            var moveModule = actor.FetchModule<MovementModule>();

            AddTransition(searchState, chaseState, () => { return searchModule.HasTarget; });
            AddTransition(chaseState, searchState, () => { return !searchModule.HasTarget; });

            _initialState = searchState;
        }


    }
}