using BrightLib.StateMachine.Samples;
using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public class SimpleFSMSample : MonoBehaviour
    {   
        protected FSM _fsm;

        private void Awake()
        {
            _fsm = new LightSwitchFSM();
        }

        private void Start()
        {
            if (_fsm == null) enabled = false;
            _fsm.ChangeToStartState();
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