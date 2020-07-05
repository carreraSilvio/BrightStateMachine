using System;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Transition where you simply leave the current state if the condition is met
    /// </summary>
    internal class ReturnTransition : Transition
    {
        public ReturnTransition(Func<bool> func) : base(null, func)
        {
        }
    }


}