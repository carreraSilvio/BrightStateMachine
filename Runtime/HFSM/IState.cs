namespace BrightLib.StateMachine.Runtime
{
    public interface IState
    {
        public void Enter();

        public void Update();

        public void LateUpdate();

        public void Exit();
        
    }
    
}