using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    internal class WaitState : ObjectState<BattleSystem>
    {
        public WaitState(BattleSystem component) : base(component)
        {
        }
    }
}