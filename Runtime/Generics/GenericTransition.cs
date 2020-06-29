using System;

namespace BrightLib.StateMachine.Runtime
{
    public class GenericTransition<T> where T : GenericState
    {
        private readonly T _target;
        private readonly Func<bool> _condition;

        public T Target => _target;
        public Func<bool> Condition => _condition;
        

        public GenericTransition(T target, Func<bool> func)
        {
            _target = target;
            _condition = func;
        }
    }
}