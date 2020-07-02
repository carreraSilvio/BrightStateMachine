﻿using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// <see cref="FSM"/> with a generic field added to ease state creation
    /// </summary>
    public class FSM<T0> : FSM where T0 : Component
    {
        protected T0 _component;

        public T0 Component => _component;
        public GameObject GameObject => _component.gameObject;

        public FSM(T0 component) : base()
        {
            _component = component;
        }

        /// <summary>
        /// Creates a state of type T1 and injects the <see cref="Component"/>
        /// </summary>
        public T1 CreateState<T1>() where T1 : State<T0>
        {
            return (T1) System.Activator.CreateInstance(typeof(T1), new object[] { _component });
        }

    }
}