using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public class PlayerFSMRunner : FSMRunner<Actor>
    {
        private void Start()
        {
            _fsm = new PlayerFSM(_owner);
            _fsm.ChangeToStartState();
        }

    }
}