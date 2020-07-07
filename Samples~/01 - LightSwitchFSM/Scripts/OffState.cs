using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples.FSMSample
{
    public class OffState : State
    {

        public override void Enter()
        {
            Log("Light OFF");
        }
    }
}