using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples.HFSMSample
{
    public class OvenHFSMMain : MonoBehaviour
    {
        protected OvenHFSM _fsm;

        private void Start()
        {
            _fsm = new OvenHFSM();
            _fsm.ChangeToStartState();
            _fsm.OnStateExit += HandleStatExit;
            _fsm.OnStateEnter += HandleStateEnter;
        }

        private void HandleStatExit(HFSMState state)
        {
            Debug.Log($"Exit State \t{state.FullName()}");
        }

        private void HandleStateEnter(HFSMState state)
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