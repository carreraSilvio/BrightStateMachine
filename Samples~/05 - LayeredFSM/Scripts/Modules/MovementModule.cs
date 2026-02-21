using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class MovementModule : ActorModule
    {
        public float SpeedModifier { get; set; } = 1.0f;
        [SerializeField]
        private float _speed = 50f;

        public void Move(Vector2 direction)
        {
            Move(direction.x, direction.y);
        }
        public void Move(float x, float y)
        {
            var pos = Actor.transform.position;
            pos.x += x * _speed * SpeedModifier * Time.deltaTime;
            pos.y += y * _speed * SpeedModifier * Time.deltaTime;

            Actor.transform.position = pos;
        }
    }
}
