using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class BattleSystem : FSM<BattleSystem>
    {
        public void Awake()
        {
            OnStateExit += HandleStatExit;
            OnStateEnter += HandleStateEnter;

            var playerTurn = CreateState<PlayerTurnState>();
            var waitState = CreateState<WaitState>(); 
            var enemyTurn = CreateState<EnemyTurnState>();

            AddTransition(playerTurn, waitState, () => Input.GetKeyDown(KeyCode.Space));
            AddTransition(waitState, enemyTurn, () => Input.GetKeyDown(KeyCode.Space));
            AddTransition(enemyTurn, playerTurn, () => Input.GetKeyDown(KeyCode.Space));

            SetInitialState(playerTurn);

            ChangeToInitialState();
        }


        private void HandleStatExit(State state)
        {
            Debug.Log($"Exit State \t{state.FullName()}");
        }

        private void HandleStateEnter(State state)
        {
            Debug.Log($"Enter State \t{state.FullName()}");
        }
    }

    public class BattleSystemState : State<BattleSystem> { public BattleSystemState(BattleSystem component) : base(component) { } };
}