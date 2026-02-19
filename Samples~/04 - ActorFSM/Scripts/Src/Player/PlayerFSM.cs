namespace BrightLib.StateMachine.Samples
{
    public class PlayerFSM : ActorFSM
    {
        private void Start()
        {
            var idleState = CreateState<Idle_PlayerState>();
            var moveState = CreateState<Move_PlayerState>();

            var moveModule = Actor.FetchModule<MovementModule>();
            var inputModule = Actor.FetchModule<CharacterControllerModule>();

            AddTransition(idleState, moveState, () => { return inputModule.IsMoving; });
            AddTransition(moveState, idleState, () => { return !inputModule.IsMoving; });

            SetInitialState(idleState);
            ChangeToInitialState();
        }
        
    }
}