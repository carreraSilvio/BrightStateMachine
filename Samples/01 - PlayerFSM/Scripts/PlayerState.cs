using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public class PlayerState : State
    {
        public override void Enter()
        {
            Log("Enter" + this.GetType().Name);
        }

        public override void Exit()
        {
            Log("Exit" + this.GetType().Name);
        }
    }
}