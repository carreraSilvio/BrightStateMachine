using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class BattleSystem : FSMBehaviour<BattleSystem>
    {
        public void Start()
        {
            _fsm.OnStateExit += HandleStatExit;
            _fsm.OnStateEnter += HandleStateEnter;

            var playerTurn = _fsm.CreateState<PlayerTurnState>();
            var waitState = _fsm.CreateState<WaitState>(); 
            var enemyTurn = _fsm.CreateState<EnemyTurnState>();

            _fsm.AddTransition(playerTurn, waitState, () => Input.GetKeyDown(KeyCode.Space));
            _fsm.AddTransition(waitState, enemyTurn, () => Input.GetKeyDown(KeyCode.Space));
            _fsm.AddTransition(enemyTurn, playerTurn, () => Input.GetKeyDown(KeyCode.Space));

            _fsm.SetInitialState(playerTurn);

            _fsm.ChangeToInitialState();
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