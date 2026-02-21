using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class RunState : State<Transform>
    {
        public RunState(Transform component) : base(component)
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