using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class PlayerCombatFSM : ActorFSM
    {
        private void Awake()
        {
            DisplayName = "PlayerCombatFSM";
        }
        private void Start()
        {
            var readyState = CreateState<ReadyState>();
            var shootingState = CreateState<ShootingState>();
            var reloadingState = CreateState<ReloadingState>();

            var moveModule = Actor.FetchModule<MovementModule>();
            var inputModule = Actor.FetchModule<CharacterControllerModule>();
            var shooterModule = Actor.FetchModule<ShooterModule>();

            AddTransition(readyState, shootingState, () => { return inputModule.IsShootButtonPressed; });
            AddTransition(shootingState, readyState, () => { return !inputModule.IsShootButtonPressed; });
            AddTransition(shootingState, reloadingState, () => { return shooterModule.IsOutOfAmmo; });
            AddTransition(reloadingState, readyState, () => { return !shooterModule.IsOutOfAmmo; });

            SetInitialState(readyState);
            ChangeToInitialState();
        }
    }

}