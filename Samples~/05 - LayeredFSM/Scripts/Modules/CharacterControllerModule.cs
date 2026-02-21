using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class CharacterControllerModule : ActorModule
    {
        public bool IsMoving { get; private set; }
        public bool IsShootButtonPressed { get; private set; }
        public float HorizontalMove => _horizontalAxis;
        public float VerticalMove => _verticalAxis;

        private float _horizontalAxis;
        private float _verticalAxis;

        private void Update()
        {
            _horizontalAxis = Input.GetAxisRaw("Horizontal");
            _verticalAxis = Input.GetAxisRaw("Vertical");

            Vector2 input = new Vector2(_horizontalAxis, _verticalAxis);

            IsMoving = input != Vector2.zero;
            IsShootButtonPressed = Input.GetKey(KeyCode.Space);

            if (input != Vector2.zero)
            {
                Actor.FaceDirection = input;
            }

        }
    }
}
