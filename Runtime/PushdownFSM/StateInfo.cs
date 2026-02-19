using System.Collections.Generic;

namespace BrightLib.StateMachine.Runtime
{
    public struct StateInfo
    {
        public State state;

        public List<Transition> pushTransitions;
        public List<Transition> popTransitions;
    }
}