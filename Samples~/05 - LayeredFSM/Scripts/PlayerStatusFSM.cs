using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    [RequireComponent(typeof(Actor))]
    public class PlayerStatusFSM : ActorFSM
    {
        private void Awake()
        {
            DisplayName = "PlayerStatusFSM";
        }

        private void Start()
        {
            var normalState = CreateState<NormalState>();
            var slowedState = CreateState<SlowedState>();

            var lifeModule = Actor.FetchModule<StatusModule>();

            AddTransition(normalState, slowedState, () => { return lifeModule.IsSlowed; });
            AddTransition(slowedState, normalState, () => { return !lifeModule.IsSlowed; });

            SetInitialState(normalState);
            ChangeToInitialState();
        }
    }

}