using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public class EnemyFSMRunner : FSMRunner<Actor>
    {
        private void Start()
        {
            _fsm = new EnemyFSM(_owner);
            _fsm.ChangeToStartState();
        }

    }
}