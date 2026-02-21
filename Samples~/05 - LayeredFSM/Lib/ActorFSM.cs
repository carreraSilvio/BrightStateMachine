using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class ActorFSM : FSM<Actor>
    {
        public Actor Actor => Component;
    }
}
