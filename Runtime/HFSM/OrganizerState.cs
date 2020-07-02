namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// A hierarchical state that is meant to be used as a parent or parent and child of other states to reduce transition redundancy
    /// </summary>
    public abstract class OrganizerState : IState
    {
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public void LateUpdate()
        {
            
        }

        public void Update()
        {
           
        }

        public virtual IState GetLeafChild()
        {
            return this;
        }

    }
}