using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    internal class EnemyTurnState : ObjectState<BattleSystem>
    {
        public EnemyTurnState(BattleSystem component) : base(component)
        {
        }
    }
}