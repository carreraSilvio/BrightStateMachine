using System;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Transition where you quit the current state and return to previous on the stack
    /// </summary>
    internal class QuitTransition : Transition
    {
        public QuitTransition(Func<bool> func) : base(null, func)
        {
        }
    }


}