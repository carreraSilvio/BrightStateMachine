﻿using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Extend from this component to update your fsm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FSMRunner<T> : MonoBehaviour where T : Component
    {
        [SerializeField]
        protected T _component = default;

        protected FSM<T> _fsm;

        private void Update()
        {
            _fsm.Update();
        }

        private void LateUpdate()
        {
            _fsm.LateUpdate();
        }
    }
}