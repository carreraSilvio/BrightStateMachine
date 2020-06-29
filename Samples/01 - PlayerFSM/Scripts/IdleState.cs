namespace BrightLib.StateMachine.Samples
{
    public class IdleState : PlayerState
    {
        public IdleState()
        {
            
        }

        public override void Enter()
        {
            Log("Enter" + nameof(IdleState));
        }
    }
}