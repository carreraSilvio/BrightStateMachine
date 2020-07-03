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
            var stateName = (state.HasParentState ? state.ParentState.GetType().Name + "." : "") + state.GetType().Name;
            Debug.Log($"Exit State \t{stateName}");
        }

        private void HandleStateEnter(HFSMState state)
        {
            var stateName =  (state.HasParentState ? state.ParentState.GetType().Name + "." : "") +  state.GetType().Name;
            Debug.Log($"Enter State \t{stateName}");
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