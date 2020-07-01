using BrightLib.StateMachine.Samples;
using System;
using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public class SimpleFSMSample : MonoBehaviour
    {
        public Light _light;
        protected FSM _fsm;

        private void Start()
        {
            _fsm = new LightSwitchFSM();
            _fsm.ChangeToStartState();
            _fsm.OnStateChange += HandleStateChange;
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