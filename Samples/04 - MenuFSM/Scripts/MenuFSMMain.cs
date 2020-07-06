using BrightLib.StateMachine.Runtime;
using System;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class MenuFSMMain : MonoBehaviour
    {
        protected MenuFSM _fsm;
        private void Start()
        {
            _fsm = new MenuFSM();
            _fsm.OnStateExit += HandleStatExit;
            _fsm.OnStateEnter += HandleStateEnter;
            _fsm.OnStateFocus += HandleStateFocus;
            _fsm.OnStateObscure += HandleStateObscure;

            _fsm.ChangeToInitialState();
        }

        private void HandleStateObscure(State state)
        {
            Debug.Log($"Obscure State \t{state.FullName()}");
        }

        private void HandleStateFocus(State state)
        {
            Debug.Log($"Focus State \t{state.FullName()}");
        }

        private void HandleStatExit(State state)
        {
            Debug.Log($"Exit State \t{state.FullName()}");
        }

        private void HandleStateEnter(State state)
        {
            Debug.Log($"Enter State \t{state.FullName()}");
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