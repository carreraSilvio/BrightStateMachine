using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    [RequireComponent(typeof(Collider2D))]
    public class SearchModule : ActorModule
    {
        [SerializeField]
        private string _targetTag = "Player";

        private Actor _target;

        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.transform.position;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var actor = collision.GetComponentInParent<Actor>();
            if (actor.CompareTag(_targetTag))
            {
                _target = actor;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_target == null) return;

            var actor = collision.GetComponentInParent<Actor>();
            if (actor.CompareTag(_target.tag))
            {
                _target = null;
            }
        }
    }
}