using BrightLib.StateMachine.Runtime;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An FSM or HSFM that has as stack of states and the current is always the one on top. Good for making menus.
/// </summary>
public class PushdownFSM
{
    public Stack<State> _stack;

    public State CurrentState => _stack.Peek();

}
