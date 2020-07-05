using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples.HFSMSample
{
    public class MenuFSMMain : MonoBehaviour
    {
        protected MenuFSM _fsm;

        private void Start()
        {
            _fsm = new MenuFSM();
            _fsm.OnStateExit += HandleStatExit;
            _fsm.OnStateEnter += HandleStateEnter;

            _fsm.ChangeStateToInitialState();
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