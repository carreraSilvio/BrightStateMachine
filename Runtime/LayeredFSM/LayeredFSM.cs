using BrightLib.StateMachine.Runtime;

/// <summary>
/// Manages multiple FSMs
/// </summary>
public class LayeredFSM
{
    //A layered FSM has at least one FSM
    public FSM baseFSM;

    public FSM[] fsms;

}
