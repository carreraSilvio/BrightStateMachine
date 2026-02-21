using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class PlayerLocomotionFSM : ActorFSM
    {
        private void Awake()
        {
            DisplayName = "PlayerLocomotionFSM";
        }

        private void Start()
        {
            Actor.FaceDirection = new Vector2( 1.0f, 0.0f );
            var idleState = CreateState<IdleState>();
            var moveState = CreateState<MoveState>();

            var moveModule = Actor.FetchModule<MovementModule>();
            var inputModule = Actor.FetchModule<CharacterControllerModule>();

            AddTransition(idleState, moveState, () => { return inputModule.IsMoving; });
            AddTransition(moveState, idleState, () => { return !inputModule.IsMoving; });

            SetInitialState(idleState);
            ChangeToInitialState();
        }
    }

}