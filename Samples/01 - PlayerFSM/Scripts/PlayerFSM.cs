using BrightLib.StateMachine.Runtime;
using System;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class PlayerFSM : FSM
    {
        public PlayerFSM()
        {
            var idleState = new IdleState();
            var moveState = new MoveState();

            idleState.AddTransition(moveState, () => { return Input.GetKey(KeyCode.Space);});
            moveState.AddTransition(idleState, () => { return Input.GetKeyUp(KeyCode.Space); });

            startState = idleState;
        }

    }
}