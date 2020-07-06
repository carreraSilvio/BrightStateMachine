using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public abstract class ActorFSMBehaviour : FSMBehaviour<Actor>
    {
        public Actor Actor => Component;
    }
}