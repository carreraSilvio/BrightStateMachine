using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public class PlayerFSMRunner : HFSMRunner<Actor>
    {
        private void Start()
        {
            _fsm = new PlayerFSM(_component);
            _fsm.ChangeToStartState();
        }

    }
}