using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public abstract class ActorFSM : FSM<Actor>
    {
        public Actor Actor => Component;
    }
}