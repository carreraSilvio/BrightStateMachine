using System;
using UnityEngine.Events;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Zero argument State
    /// </summary>
    public abstract class State
    {
        public virtual void Enter()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void LateUpdate()
        {

        }

        public virtual void Exit()
        {

        }

        public void Log(object message) => UnityEngine.Debug.Log(message);
    }
}