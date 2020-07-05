using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    internal class PlayerTurnState : ObjectState<BattleSystem>
    {
        public PlayerTurnState(BattleSystem component) : base(component)
        {
        }
    }
}