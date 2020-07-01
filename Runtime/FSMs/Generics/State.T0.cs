using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public abstract class State<T> where T : Component
    {
        private T _owner;
        
        public T Owner => _owner;

        public State(T owner)
        {
            _owner = owner;
        }

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