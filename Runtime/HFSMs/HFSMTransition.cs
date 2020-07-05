using System;

namespace BrightLib.StateMachine.Runtime
{
    public class HFSMTransition
    {
        protected readonly HFSMState _target;
        protected readonly Func<bool> _condition;

        public HFSMState Target => _target;
        public Func<bool> Condition => _condition;

        public HFSMTransition(HFSMState target, Func<bool> func)
        {
            _target = target;
            _condition = func;
        }
    }
}