using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class HierarchicalFSM : FSM
    {
        private void Start()
        {
            var attackState = new AttackState(transform);

            var locomotionState = new LocomotionState();
            var idleState = new IdleState();
            var walkState = new WalkState(transform);
            var runState = new RunState(transform);

            locomotionState.AddChildAsInitialState(idleState);
            locomotionState.AddChild(walkState);
            locomotionState.AddChild(runState);

            AddTransition(attackState, locomotionState, () => { return TimeElapsedInCurrentState > 1.0f; });
            AddTransition(locomotionState, attackState, () => { return Input.GetKeyDown(KeyCode.Space); });
            AddTransition(idleState, walkState, () => { return Mathf.Abs(Input.GetAxis("Horizontal")) > 0.0f; });
            AddTransition(walkState, idleState, () => { return Mathf.Abs(Input.GetAxis("Horizontal")) == 0.0f; });
            AddTransition(walkState, runState, () => { return TimeElapsedInCurrentState > 0.5f; });
            AddTransition(runState, walkState, () => { return Mathf.Abs(Input.GetAxis("Horizontal")) < 1.0f; });

            _initialState = idleState;
      
            ChangeToInitialState();
            OnStateEnter += HandleStateEnter;
            OnStateExit += HandleStateExit;

            DisplayName = "HierachicalFSM";
            LogTransitions = true;
        }

        private void HandleStateEnter(State state)
        {
            //Debug.Log($"Enter State \t{state.GetFullName()}");
        }

        private void HandleStateExit(State state)
        {
            //Debug.Log($"Exit State \t{state.GetFullName()}");
        }

    }


}