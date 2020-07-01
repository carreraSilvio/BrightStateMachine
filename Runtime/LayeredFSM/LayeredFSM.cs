using BrightLib.StateMachine.Runtime;
using UnityEngine;

/// <summary>
/// Manages multiple FSMs or HFSMs. Has internal EventSystem that FSM states can fire to alter other fsm.
/// </summary>
public class LayeredFSM
{
    //A layered FSM has at least one FSM
    public FSM baseFSM;

    public FSM[] fsms;
    
}
