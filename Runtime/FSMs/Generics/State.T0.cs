using System;
using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public abstract class State<T0> : State where T0 : Component
    {
        public new event Action<T0> OnEnter;
        public new event Action<T0> OnExit;

        private readonly T0 _component;

        public T0 Component => _component;
        public GameObject GameObject => _component.gameObject;

        public State(T0 component) : base()
        {
            _component = component;
        }

        public State(T0 component, string displayName) : base(displayName)
        {
            _component = component;
        }

        internal override void OnEnterInvoke() => OnEnter?.Invoke(_component);
        internal override void OnExitInvoke() => OnExit?.Invoke(_component);
    }
}