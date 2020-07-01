using System;

namespace BrightLib.StateMachine.Runtime
{
    public class Transition<T0> where T0 : State
    {
        protected readonly T0 _target;
        protected readonly Func<bool> _condition;

        public T0 Target => _target;
        public Func<bool> Condition => _condition;

        public Transition(T0 target, Func<bool> func)
        {
            _target = target;
            _condition = func;
        }
    }
}