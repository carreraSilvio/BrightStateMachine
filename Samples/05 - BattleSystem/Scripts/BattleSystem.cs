using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class BattleSystem
    {
        public BattleSystemFSM fsm;

        public BattleSystem()
        {
            fsm = new BattleSystemFSM(this);

            fsm.OnStateExit += HandleStatExit;
            fsm.OnStateEnter += HandleStateEnter;

            fsm.ChangeStateToInitialState();
        }


        private void HandleStatExit(State state)
        {
            Debug.Log($"Exit State \t{state.FullName()}");
        }

        private void HandleStateEnter(State state)
        {
            Debug.Log($"Enter State \t{state.FullName()}");
        }

        public void Update()
        {
            fsm.Update();
        }
    }
}