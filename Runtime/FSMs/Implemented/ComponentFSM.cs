using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public class ComponentFSM : FSM<Component>
    {
        public ComponentFSM(Component owner) : base(owner)
        {
        }

    }
}