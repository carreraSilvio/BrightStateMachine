using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class BattleSystem
    {
        public ObjectFSM<BattleSystem> fsm;

        public BattleSystem()
        {
            fsm = new ObjectFSM<BattleSystem>(this);

            fsm.OnStateExit += HandleStatExit;
            fsm.OnStateEnter += HandleStateEnter;

            var playerTurn = fsm.CreateState<PlayerTurnState>();
            var waitState = fsm.CreateState<WaitState>(); 
            var enemyTurn = fsm.CreateState<EnemyTurnState>();

            fsm.AddTransition(playerTurn, waitState, () => Input.GetKeyDown(KeyCode.Space));
            fsm.AddTransition(waitState, enemyTurn, () => Input.GetKeyDown(KeyCode.Space));
            fsm.AddTransition(enemyTurn, playerTurn, () => Input.GetKeyDown(KeyCode.Space));

            fsm.SetInitialState(playerTurn);

            fsm.ChangeToInitialState();
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