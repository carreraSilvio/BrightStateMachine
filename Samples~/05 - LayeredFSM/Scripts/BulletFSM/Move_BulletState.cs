namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class Move_BulletState : ActorState
    {
        public Move_BulletState(Actor actor) : base(actor)
        {
        }

        public override void Tick()
        {
            Actor.FetchModule<MovementModule>().Move(Actor.FaceDirection);
        }
    }
}
