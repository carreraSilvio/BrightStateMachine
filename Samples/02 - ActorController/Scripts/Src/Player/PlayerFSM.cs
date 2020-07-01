using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public class PlayerFSM : FSM<Actor>
    {
        public PlayerFSM(Actor owner) : base(owner)
        {
            var idleState = CreateState<IdleState>();
            var moveState = CreateState<MoveState>();

            var moveModule = owner.FetchModule<MovementModule>();

            AddTransition(idleState, moveState, () => { return moveModule.IsMoving; });
            //AddTransition(moveState, idleState, () => { return !moveModule.IsMoving; });

            _startState = idleState;
        }

        
    }
}