using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class OvenHFSMMain : MonoBehaviour
    {
        protected MenuFSM _fsm;

        private void Start()
        {
            _fsm = new MenuFSM();
            _fsm.ChangeToInitialState();
            _fsm.OnStateExit += HandleStatExit;
            _fsm.OnStateEnter += HandleStateEnter;
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