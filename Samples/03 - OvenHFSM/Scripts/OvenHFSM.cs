using BrightLib.StateMachine.Runtime;
using BrightLib.StateMachine.Samples.HFSMSample;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    /// <summary>
    /// The ovenHFSM will have two states, one of the states will have two child states
    /// State Off (entry)
    /// State On 
    ///    - Child State Heating Up(entry)
    ///    - Child State Heated Up
    /// Every time you transition to OnState you'll actually enter the HeatingUpState
    /// </summary>
    public class OvenHFSM : FSM
    {
        public OvenHFSM()
        {
            var offState = new OffState();

            var onState = new OnState();
            var heatingUpState = new ControlsState();
            var heatedUpState = new HeatedUpState();

            onState.AddChildAsInitialState(heatingUpState);
            onState.AddChild(heatedUpState);

            AddTransition(offState, onState, () => { return Input.GetKeyDown(KeyCode.W); });
            AddTransition(onState, offState, () => { return Input.GetKeyDown(KeyCode.S); });
            AddTransition(heatingUpState, heatedUpState, () => { return Input.GetKeyDown(KeyCode.Space); });

            _initialState = offState;

        }

    }


}