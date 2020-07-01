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
            _fsm.OnStateChange += HandleStateChange;
        }

        private void HandleStateChange(NestedState state)
        {
            var stateName =  (state.IsChild ? state.ParentState.GetType().Name + "." : "") +  state.GetType().Name;
            Debug.Log($"Entered State {stateName}");
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