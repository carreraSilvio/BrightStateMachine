using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public class EnemyFSM : FSM<Actor>
    {
        public EnemyFSM(Actor owner) : base(owner)
        {
            var searchState = CreateState<SearchState>();
            var chaseState = CreateState<ChaseState>();

            var searchModule = owner.FetchModule<SearchModule>();
            var moveModule = owner.FetchModule<MovementModule>();

            AddTransition(searchState, chaseState, () => { return searchModule.HasTarget; });
            AddTransition(chaseState, searchState, () => { return !searchModule.HasTarget; });

            _startState = searchState;
        }


    }
}