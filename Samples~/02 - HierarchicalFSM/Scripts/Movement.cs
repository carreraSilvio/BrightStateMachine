using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private float _walkSpeed = 50f;
        [SerializeField]
        private float _runSpeed = 100f;

        public void Walk(Vector2 direction)
        {
            Walk(direction.x, direction.y);
        }
        public void Walk(float x, float y)
        {
            Move(x, y, _walkSpeed);
        }
        public void Run(float x, float y)
        {
            Move(x, y, _runSpeed);
        }

        private void Move(float x, float y, float speed)
        {
            var pos = transform.position;
            pos.x += x * speed * Time.deltaTime;
            pos.y += y * speed * Time.deltaTime;

            transform.position = pos;
        }

    }

}