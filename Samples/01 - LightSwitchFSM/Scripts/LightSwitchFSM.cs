using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples.FSMSample
{
    public class LightSwitchFSM : FSM
    {
        public LightSwitchFSM()
        {
            var offState = new OffState();
            var onState = new OnState();

            AddTransition(offState, onState, () => { return Input.GetKeyDown(KeyCode.Space);});
            AddTransition(onState, offState, () => { return Input.GetKeyDown(KeyCode.Space); });

            _initialState = offState;
        }

    }
}