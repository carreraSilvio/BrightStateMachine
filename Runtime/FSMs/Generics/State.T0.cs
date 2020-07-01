using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public abstract class State<T> where T : Component
    {
        private T _component;
        
        public T Component => _component;
        public GameObject GameObject => _component.gameObject;

        public State(T component)
        {
            _component = component;
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