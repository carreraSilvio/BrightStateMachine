using System;
using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    public abstract class State
    {
        protected List<Transition> _transitions = new List<Transition>();

        public List<Transition> Transitions => _transitions;

        public virtual void Enter()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Exit()
        {

        }

        public void AddTransition(State target, Func<bool> condition) => _transitions.Add(new Transition(target, condition));

        public void Log(object message) => UnityEngine.Debug.Log(message);

    }
}