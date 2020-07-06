namespace BrightLib.StateMachine.Runtime
{
    public abstract class ObjectState<T0> : State where T0 : class
    {
        private readonly T0 _targetObject;

        public T0 TargetObject => _targetObject;

        public ObjectState(T0 targetObject)
        {
            _targetObject = targetObject;
        }
    }
}