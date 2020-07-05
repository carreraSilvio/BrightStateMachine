using System.ComponentModel;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// <see cref="FSM"/> with easy access to the component and game object
    /// </summary>
    public class ObjectFSM<T0> : FSM where T0 : class
    {
        protected T0 _targetObject;

        public T0 TargetObject => _targetObject;

        public ObjectFSM(T0 targetObject) : base()
        {
            _targetObject = targetObject;
        }

    }
}