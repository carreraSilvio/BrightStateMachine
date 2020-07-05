using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples.FSMSample
{
    public class OnState : HFSMState
    {
        public override void Enter()
        {
            Log("Light ON");
        }
    }
}