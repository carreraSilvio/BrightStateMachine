using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public class EnemyFSMRunner : HFSMRunner<Actor>
    {
        private void Start()
        {
            _fsm = new EnemyFSM(_component);
            _fsm.ChangeToStartState();
        }

    }
}