using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    [RequireComponent(typeof(Collider2D))]
    public class SearchModule : ActorModule
    {
        [SerializeField]
        private string _targetTag = "Player";
        [SerializeField]
        private float _stoppingTreshold = 2f;

        private Actor _target;

        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.transform.position;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var otherActor = collision.GetComponentInParent<Actor>();
            if (otherActor.CompareTag(_targetTag) )
            {
                float distance = (otherActor.transform.position - Actor.transform.position).magnitude;
                if (distance > _stoppingTreshold)
                {
                    _target = otherActor;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            var otherActor = collision.GetComponentInParent<Actor>();
            if (otherActor.CompareTag(_targetTag))
            {
                float distance = (otherActor.transform.position - Actor.transform.position).magnitude;
                if (distance > _stoppingTreshold)
                {
                    _target = otherActor;
                }
                else
                {
                    _target = null;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_target == null)
            {
                return;
            }
             
            var otherActor = collision.GetComponentInParent<Actor>();
            if (otherActor.CompareTag(_target?.tag))
            {
                _target = null;
            }
        }
    }
}