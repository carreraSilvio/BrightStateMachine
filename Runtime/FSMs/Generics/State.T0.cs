using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public abstract class State<T0> : State where T0 : Component
    {
        private T0 _component;
        
        public T0 Component => _component;
        public GameObject GameObject => _component.gameObject;

        public State(T0 component)
        {
            _component = component;
        }
    }
}