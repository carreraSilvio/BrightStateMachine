﻿using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class ActorModule : MonoBehaviour
    {
        private Actor _actor;

        protected Actor Actor => _actor;
        public Vector3 ActorPosition => _actor.transform.position;

        private void Awake()
        {
            _actor = GetComponentInParent<Actor>();
        }
    }
}