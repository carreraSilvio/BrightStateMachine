using System;
using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// FSM wrapper behaviour
    /// </summary>
    public abstract class FSMBehaviour<T0> : MonoBehaviour where T0 : Component
    {
        [SerializeField]
        protected T0 _component = default;

        protected FSM<T0> _fsm;

        public T0 Component => _component;

        /// <summary>
        /// <inheritdoc cref="FSM{T0}.CreateState"/>
        /// </summary>
        public T1 CreateState<T1>() where T1 : State<T0> => _fsm.CreateState<T1>();

        /// <summary>
        /// <inheritdoc cref="FSM.AddTransition"/>
        /// </summary>
        public void AddTransition(State from, State to, Func<bool> condition) => _fsm.AddTransition(from, to, condition);

        /// <summary>
        /// <inheritdoc cref="FSM.AddAnyTransition"/>
        /// </summary>
        public void AddAnyTransition(State to, Func<bool> condition) => _fsm.AddAnyTransition(to, condition);

        /// <summary>
        /// <inheritdoc cref="FSM.SetInitialState"/>
        /// </summary>
        public void SetInitialState(State initialState) => _fsm.SetInitialState(initialState);

        /// <summary>
        /// <inheritdoc cref="FSM.ChangeToInitialState"/>
        /// </summary>
        public void ChangeToInitialState() => _fsm.ChangeToInitialState();

        private void Awake()
        {
            _fsm = new FSM<T0>(_component);
        }

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