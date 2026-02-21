using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class ActorState : State<Actor>
    {
        public Actor Actor => Component;

        public ActorState(Actor actor) : base(actor)
        {
        }
    }
}
