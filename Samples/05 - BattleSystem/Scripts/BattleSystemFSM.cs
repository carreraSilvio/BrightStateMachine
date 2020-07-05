using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class BattleSystemFSM : ObjectFSM<BattleSystem>
    {
        public BattleSystemFSM(BattleSystem targetObject) : base(targetObject)
        {
            var playerTurn = new PlayerTurnState(targetObject);
            var waitState = new WaitState(targetObject);
            var enemyTurn = new EnemyTurnState(targetObject);

            AddTransition(playerTurn, waitState, () => Input.GetKeyDown(KeyCode.Space));
            AddTransition(waitState, enemyTurn, () => Input.GetKeyDown(KeyCode.Space));
            AddTransition(enemyTurn, playerTurn, () => Input.GetKeyDown(KeyCode.Space));

            _initialState = playerTurn;
        }
    }
}