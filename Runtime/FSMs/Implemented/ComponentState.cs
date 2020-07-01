using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    public class ComponentState : State<Component>
    {
        public Component Component => Owner;

        public GameObject GameObject => Owner.gameObject;

        public ComponentState(Component owner) : base(owner)
        {
        }
    }
}