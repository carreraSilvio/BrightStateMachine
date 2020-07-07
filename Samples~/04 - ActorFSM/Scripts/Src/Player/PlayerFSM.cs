namespace BrightLib.StateMachine.Samples
{
    public class PlayerFSM : ActorFSM
    {

        private void Start()
        {
            var idleState = CreateState<IdleState>();
            var moveState = CreateState<MoveState>();

            var moveModule = Actor.FetchModule<MovementModule>();

            AddTransition(idleState, moveState, () => { return moveModule.IsMoving; });
            //AddTransition(moveState, idleState, () => { return !moveModule.IsMoving; });

            SetInitialState(idleState);
            ChangeToInitialState();
        }

        
    }
}