using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// A FSM that allows you to stack states and keep a history
    /// </summary>
    public class PushdownFSM : FSM
    {
        private Stack<State> _stack;

        public PushdownFSM()
        {
            _stack = new Stack<State>();
        }

        public void PushState(State state)
        {
            _stack.Push(_currentState);
            _currentState = state;
            _currentState.Enter();
        }

        public void PopState()
        {
            _currentState.Exit();
            _currentState = _stack.Pop();
        }

        public override void ChangeState(State targetState)
        {
            while (_stack.Peek() != null)
            {
                _stack.Pop().Exit();
            }
            base.ChangeState(targetState);
        }

        //public void AddToPreviousTransition(State to, Func<bool> condition)
        //{
        //    _anyStateTransitions.Add(new Transition(to, condition));
        //}

    }
}