using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public abstract class HFSMState<T0> : HFSMState where T0 : Component
    {
        private T0 _component;
        
        public T0 Component => _component;
        public GameObject GameObject => _component.gameObject;

        public HFSMState(T0 component)
        {
            _component = component;
        }
    }
}