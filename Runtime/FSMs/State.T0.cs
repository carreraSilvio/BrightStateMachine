using System;
using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public abstract class State<T0> : State where T0 : Component
    {
        public T0 Component => _component;
        public GameObject GameObject => _component.gameObject;
        private readonly T0 _component;

        public State(T0 component) : base()
        {
            _component = component;
        }

        public State(T0 component, string displayName) : base(displayName)
        {
            _component = component;
        }
    }
}