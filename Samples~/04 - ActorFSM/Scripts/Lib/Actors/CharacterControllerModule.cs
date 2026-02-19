using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class CharacterControllerModule : ActorModule
    {
        public bool IsMoving => _isMoving;
        private bool _isMoving;

        private float _horizontalAxis;
        private float _verticalAxis;

        private void Update()
        {
            _horizontalAxis = Input.GetAxis("Horizontal");
            _verticalAxis = Input.GetAxis("Vertical");

            _isMoving = (_horizontalAxis != 0) || (_verticalAxis != 0);
        }
    }
}