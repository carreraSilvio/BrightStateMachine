using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class SlowedState : ActorState
    {
        public SlowedState(Actor actor) : base(actor)
        {
        }

        public override void Enter()
        {
            Actor.FetchModule<StatusModule>().ApplySlowStatus();
            Actor.FetchModule<MovementModule>().SpeedModifier = Actor.FetchModule<StatusModule>().SlowSpeedModifier;
            Actor.GetComponentInChildren<SpriteRenderer>().color = Color.saddleBrown;
            //disable combat while slowed
            FSMEventBus.SetFSMRunning<PlayerCombatFSM>(false);
        }

        public override void Exit()
        {
            Actor.FetchModule<StatusModule>().ClearSlowStatus();
            Actor.FetchModule<MovementModule>().SpeedModifier = 1.0f;
            Actor.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            FSMEventBus.SetFSMRunning<PlayerCombatFSM>(true);
        }
    }
}
