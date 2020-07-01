using System;

namespace BrightLib.StateMachine.Runtime
{
    public class Transition
    {
        private readonly State _target;
        private readonly Func<bool> _condition;

        public State Target => _target;
        public Func<bool> Condition => _condition;
        

        public Transition(State target, Func<bool> func)
        {
            _target = target;
            _condition = func;
        }
    }
}