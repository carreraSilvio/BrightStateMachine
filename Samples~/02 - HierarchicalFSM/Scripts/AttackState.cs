using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class AttackState : State<Transform>
    {
        public AttackState(Transform component) : base(component)
        {
        }

        public override void Enter()
        {
            GameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }

        public override void Exit()
        {
            GameObject.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        }
    }
}