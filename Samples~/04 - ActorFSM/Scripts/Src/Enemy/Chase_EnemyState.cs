using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class Chase_EnemyState : ActorState
    {
        private SearchModule _searchModule;
        private MovementModule _movementModule;

        public Chase_EnemyState(Actor actor) : base(actor)
        {
        }

        public override void Enter()
        {
            if(_searchModule == null)
            {
                _searchModule = Actor.FetchModule<SearchModule>();
            }
            if (_movementModule == null)
            {
                _movementModule = Actor.FetchModule<MovementModule>();
            }
        }

        public override void Tick()
        {
            var direction = (_searchModule.TargetPosition - Actor.transform.position).normalized;
            _movementModule.Move(direction);
        }
    }
}