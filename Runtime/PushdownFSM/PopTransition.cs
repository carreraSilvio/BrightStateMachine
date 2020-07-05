using System;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Transition where you simply leave the current state if the condition is met
    /// </summary>
    internal class PopTransition : Transition
    {
        public PopTransition(State target, Func<bool> func) : base(null, func)
        {
        }

        public PopTransition(Func<bool> func) : base(null, func)
        {
        }
    }


}