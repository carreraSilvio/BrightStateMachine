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
        private FSM[] _layers;

        private bool _firstFrame = true;

        private void Update()
        {
            if(_firstFrame)
            {
                _layers = GetComponents<FSM>()
                .OrderByDescending(fsm => fsm.Priority)
                .ToArray();

                foreach (var fsm in _layers)
                {
                    fsm.ChangeToInitialState();
                }
                _firstFrame = false;
            }

            if (_layers == null)
            {
                return;
            }
            foreach (var fsm in _layers)
            {
                fsm.Tick();
            }
        }

        private void LateUpdate()
        {
            if(_layers == null)
            {
                return;
            }
            foreach (var fsm in _layers)
            {
                fsm.LateTick();
            }
        }

        private void FixedUpdate()
        {
            if (_layers == null)
            {
                return;
            }
            foreach (var fsm in _layers)
            {
                fsm.FixedTick();
            }
        }

    }
}