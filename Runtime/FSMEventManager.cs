using System;

namespace BrightLib.StateMachine.Runtime
{
    public sealed class FSMEventManager
    {
        public event Action<Type, bool> OnSetFSMRunning;

        public void SetFSMRunning<T>(bool running) where T : FSM
        {
            OnSetFSMRunning?.Invoke(typeof(T), running);
        }
        
    }
}
