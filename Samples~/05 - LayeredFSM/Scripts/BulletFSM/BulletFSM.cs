using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class BulletFSM : ActorFSM
    {
        private void Awake()
        {
            DisplayName = "BulletFSM";
        }

        private void Start()
        {
            var moveState = CreateState<Move_BulletState>();

            SetInitialState(moveState);
            ChangeToInitialState();
        }
    }
}
