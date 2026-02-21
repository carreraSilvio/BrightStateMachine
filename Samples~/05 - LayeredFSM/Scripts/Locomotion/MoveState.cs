using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class MoveState : ActorState
    {
        public MoveState(Actor actor) : base(actor)
        {
        }

        public override void Tick()
        {
            Actor.FetchModule<MovementModule>().Move(
                Actor.FetchModule<CharacterControllerModule>().HorizontalMove,
                Actor.FetchModule<CharacterControllerModule>().VerticalMove);
        }
    }
}
