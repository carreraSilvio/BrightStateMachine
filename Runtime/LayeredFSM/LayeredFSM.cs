using System.Linq;
using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Manages multiple FSMs
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class LayeredFSM : MonoBehaviour
    {
        private FSM[] _fsms;

        private bool _firstFrame = true;

        private void Update()
        {
            if(_firstFrame)
            {
                _fsms = GetComponents<FSM>()
                .OrderByDescending(fsm => fsm.Priority)
                .ToArray();

                foreach (var fsm in _fsms)
                {
                    fsm.ChangeToInitialState();
                }
                _firstFrame = false;
            }

            if (_fsms == null)
            {
                return;
            }
            foreach (var fsm in _fsms)
            {
                fsm.Tick();
            }
        }

        private void LateUpdate()
        {
            if(_fsms == null)
            {
                return;
            }
            foreach (var fsm in _fsms)
            {
                fsm.LateTick();
            }
        }

        private void FixedUpdate()
        {
            if (_fsms == null)
            {
                return;
            }
            foreach (var fsm in _fsms)
            {
                fsm.FixedTick();
            }
        }

    }
}