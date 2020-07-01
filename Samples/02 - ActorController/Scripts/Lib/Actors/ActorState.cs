using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public class ActorState : State<Actor>
    {
        public Actor Actor => Owner;
        public ActorState(Actor owner) : base(owner)
        {
        }
    }
}