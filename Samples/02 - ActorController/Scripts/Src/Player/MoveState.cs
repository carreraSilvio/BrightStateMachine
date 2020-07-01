using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class MoveState : ActorState
    {
        public MoveState(Actor owner) : base(owner)
        {
        }

        public override void Update()
        {
            var input = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Owner.FetchModule<MovementModule>().Move(input, vertical);
        }
    }
}