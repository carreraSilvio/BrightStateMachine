using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// <see cref="FSM"/> with easy access to the component and game object
    /// </summary>
    public class FSM<T0> : FSM where T0 : Component
    {
        private readonly T0 _component;

        public T0 Component => _component;
        public GameObject GameObject => _component.gameObject;

        public FSM(T0 component) : base()
        {
            _component = component;
        }

        /// <summary>
        /// Create a state of type <typeparamref name="T1"/> and inject the <see cref="Component"/>
        /// </summary>
        public T1 CreateState<T1>() where T1 : State<T0>
        {
            return (T1) System.Activator.CreateInstance(typeof(T1), new object[] { _component });
        }

    }
}