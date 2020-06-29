using BrightLib.StateMachine.Samples;
using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public class FSMRunner : MonoBehaviour
    {
        public string fsmClassName = "PlayerFSM";

        private FSM _fsm;

        private void Awake()
        {
            //var fsmType = Type.GetType(nameof(PlayerFSM));
            //_fsm =  Activator.CreateInstance(fsmType) as FSM;
            _fsm = new PlayerFSM();
        }

        private void Start()
        {
            _fsm.TransitionToState(_fsm.startState);
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