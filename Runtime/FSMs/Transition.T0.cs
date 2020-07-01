using System;
using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public class Transition<T> where T : UnityEngine.Object
    {
        private readonly State<T> _target;
        private readonly Func<bool> _condition;

        public State<T> Target => _target;
        public Func<bool> Condition => _condition;


        public Transition(State<T> target, Func<bool> func)
        {
            _target = target;
            _condition = func;
        }
    }
}
