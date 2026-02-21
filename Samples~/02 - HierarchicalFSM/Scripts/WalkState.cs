using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class WalkState : State<Transform>
    {
        public WalkState(Transform component) : base(component)
        {
        }

        public override void Tick()
        {
            var horizontalAxis = Input.GetAxis("Horizontal");
            var verticalAxis = Input.GetAxis("Vertical");
            GameObject.GetComponent<Movement>().Run(horizontalAxis, verticalAxis);
        }
    }
}