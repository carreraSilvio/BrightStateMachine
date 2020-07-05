using BrightLib.StateMachine.Runtime;

namespace BrightLib.StateMachine.Samples
{
    public class PlayerFSM : HFSM<Actor>
    {
        public PlayerFSM(Actor component) : base(component)
        {
            var idleState = CreateState<IdleState>();
            var moveState = CreateState<MoveState>();

            var moveModule = component.FetchModule<MovementModule>();
            
            AddTransition(idleState, moveState, () => { return moveModule.IsMoving; });
            //AddTransition(moveState, idleState, () => { return !moveModule.IsMoving; });

            _initialState = idleState;
        }

        
    }
}