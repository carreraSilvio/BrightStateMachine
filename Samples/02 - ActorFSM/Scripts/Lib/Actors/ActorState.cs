using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public class ActorState : HFSMState<Actor>
    {
        public Actor Actor => Component;
       
        public ActorState(Actor actor) : base(actor)
        {
        }
    }
}