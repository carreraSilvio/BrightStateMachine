using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class MovementModule : ActorModule
    {
        [SerializeField]
        private float _speed = 50f;

        public void Move(Vector2 direction)
        {
            Move(direction.x, direction.y);
        }
        public void Move(float x, float y)
        {
            var pos = Actor.transform.position;
            pos.x += x * _speed * Time.deltaTime;
            pos.y += y * _speed * Time.deltaTime;

            Actor.transform.position = pos;
        }

    }
}