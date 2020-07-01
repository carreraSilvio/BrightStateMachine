using BrightLib.StateMachine.Runtime;
using System;
using UnityEngine;

namespace BrightLib.StateMachine.Samples.HFSMSample
{
    /// <summary>
    /// The ovenHFSM will have two states, one of the states will have two child states
    /// State Off (entry)
    /// State On 
    ///    - Child State Heating Up(entry)
    ///    - Child State Heated Up
    /// Every time you transition to OnState you'll actually enter the HeatingUpState
    /// </summary>
    public class OvenHFSM : HFSM
    {
        public OvenHFSM()
        {
            var offState = new OffState();

            var onState = new OnState();
            var heatingUpState = new HeatingUpState();
            var heatedUpState = new HeatedUpState();

            onState.SetInitialState(heatingUpState);
            heatingUpState.SetParentState(onState);
            heatedUpState.SetParentState(onState);

            AddTransition(offState, onState, () => { return Input.GetKeyDown(KeyCode.W); });
            AddTransition(onState, offState, () => { return Input.GetKeyDown(KeyCode.S); });
            AddTransition(heatingUpState, heatedUpState, () => { return Input.GetKeyDown(KeyCode.Space); });

            _initialState = offState;

        }

    }


}