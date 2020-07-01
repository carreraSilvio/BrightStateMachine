using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class MoveState : ActorState
    {
        public MoveState(Actor actor) : base(actor)
        {
        }

        public override void Update()
        {
            var input = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Actor.FetchModule<MovementModule>().Move(input, vertical);
        }
    }
}