using System.ComponentModel;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// <see cref="FSM"/> with easy access to the component and game object
    /// </summary>
    public sealed class ObjectFSM<T0> : FSM where T0 : class
    {
        private readonly T0 _targetObject;

        public T0 TargetObject => _targetObject;

        public ObjectFSM(T0 targetObject) : base()
        {
            _targetObject = targetObject;
        }

        /// <summary>
        /// Create a state of type <typeparamref name="T1"/> and inject the <see cref="TargetObject"/>
        /// </summary>
        public T1 CreateState<T1>() where T1 : ObjectState<T0>
        {
            return (T1)System.Activator.CreateInstance(typeof(T1), new object[] { _targetObject });
        }

    }
}