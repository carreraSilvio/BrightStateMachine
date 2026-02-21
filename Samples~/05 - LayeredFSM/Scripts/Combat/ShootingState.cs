using UnityEngine;
using UnityEngine.PlayerLoop;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class ShootingState : ActorState
    {
        public ShootingState(Actor actor) : base(actor)
        {
        }

        public override void Tick()
        {
            Actor.FetchModule<ShooterModule>().Shoot(Actor.FaceDirection);
        }
    }
}
