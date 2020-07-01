namespace BrightLib.StateMachine.Runtime
{
    public abstract class State
    {
        public virtual void Enter()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void LateUpdate()
        {

        }

        public virtual void Exit()
        {

        }

        public void Log(object message) => UnityEngine.Debug.Log(message);
    }
}