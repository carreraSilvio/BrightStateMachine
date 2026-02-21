using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Generic version of <see cref="PushdownFSM"/>
    /// </summary>
    public class PushdownFSM<T0> : PushdownFSM where T0 : Component
    {
        private readonly T0 _component;

        public T0 Component => _component;
        public GameObject GameObject => _component.gameObject;

        public PushdownFSM(T0 component)
        {
            _component = component;
        }

    }
}