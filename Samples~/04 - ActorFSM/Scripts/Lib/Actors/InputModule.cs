using System;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class InputModule : ActorModule
    {
        private bool _isMoving;

        private float _horizontalAxis;
        private float _verticalAxis;

        public bool IsMoving => _isMoving;

        private void Update()
        {
            _horizontalAxis = Input.GetAxis("Horizontal");
            _verticalAxis = Input.GetAxis("Vertical");

            _isMoving = (_horizontalAxis != 0) || (_verticalAxis != 0);
        }
    }
}