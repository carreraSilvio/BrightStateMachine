using System;

namespace BrightLib.StateMachine.Runtime
{
    public class Transition
    {
        public State target;
        public Func<bool> condition;

        public Transition(State to, Func<bool> func)
        {
            this.target = to;
            this.condition = func;
        }
    }
}