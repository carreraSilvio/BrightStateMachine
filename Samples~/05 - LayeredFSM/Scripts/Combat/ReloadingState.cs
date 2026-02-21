using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class ReloadingState : ActorState
    {
        public ReloadingState(Actor actor) : base(actor)
        {
        }

        public override void Enter()
        {
            Actor.FetchModule<ShooterModule>().Reload();
        }
    }
}
