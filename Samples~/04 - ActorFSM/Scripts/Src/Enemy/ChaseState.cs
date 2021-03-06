﻿using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class ChaseState : ActorState
    {
        public ChaseState(Actor actor) : base(actor)
        {
        }

        public override void Update()
        {
            var searchModule = Actor.FetchModule<SearchModule>();

            var direction = (searchModule.TargetPosition - Actor.transform.position).normalized;


            Actor.FetchModule<MovementModule>().Move(direction);
        }
    }
}