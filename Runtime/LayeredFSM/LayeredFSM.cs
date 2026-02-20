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

        private void Start()
        {
            _layers = GetComponents<FSM>()
                .OrderByDescending(fsm => fsm.Priority)
                .ToArray();

            foreach (var fsm in _layers)
            {
                fsm.ChangeToInitialState();
            }
        }

        private void Update()
        {
            foreach (var fsm in _layers)
            {
                fsm.Tick();
            }
        }

        private void LateUpdate()
        {
            foreach (var fsm in _layers)
            {
                fsm.LateTick();
            }
        }

        private void FixedUpdate()
        {
            foreach (var fsm in _layers)
            {
                fsm.FixedTick();
            }
        }

        public T GetLayer<T>() where T : FSM
        {
            foreach (var fsm in _layers)
            {
                if (fsm is T typed)
                    return typed;
            }

            return null;
        }

    }
}