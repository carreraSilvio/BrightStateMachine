using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples.FSMSample
{
    public class SimpleFSMSample : MonoBehaviour
    {
        public Light _light;
        protected FSM _fsm;

        private void Start()
        {
            _fsm = new LightSwitchFSM();
            _fsm.ChangeToStartState();
            _fsm.OnStateEnter += HandleStateChange;
        }

        private void HandleStateChange(State state)
        {
            _light.enabled = !_light.enabled;
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