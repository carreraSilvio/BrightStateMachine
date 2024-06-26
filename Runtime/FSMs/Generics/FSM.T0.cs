﻿using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// <see cref="FSM"/> with easy access to the component and game object
    /// </summary>
    public abstract class FSM<T0> : FSM where T0 : Component
    {
        [SerializeField]
        protected T0 _component = default;

        public T0 Component => _component;

        /// <summary>
        /// Create a state of type <typeparamref name="T1"/> and inject the <see cref="Component"/>
        /// </summary>
        public T1 CreateState<T1>() where T1 : State<T0>
        {
            return (T1)System.Activator.CreateInstance(typeof(T1), new object[] { _component });
        }

    }
}