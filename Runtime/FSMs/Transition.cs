using System;

namespace BrightLib.StateMachine.Runtime
{
    public class Transition
    {
        protected readonly State _target;
        protected readonly Func<bool> _condition;

        public State Target => _target;
        public Func<bool> Condition => _condition;

        public Transition(State target, Func<bool> func)
        {
            _target = target;
            _condition = func;
        }
    }


}