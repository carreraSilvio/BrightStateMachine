using System;
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
        private readonly FSMEventManager _eventManager = new FSMEventManager();

        private bool _firstFrame = true;

        private void Start()
        {
            _fsms = GetComponents<FSM>()
            .OrderByDescending(fsm => fsm.Priority)
            .ToArray();

            foreach (var fsm in _fsms)
            {
                fsm.InjectEventManager(_eventManager);
                fsm.AutoUpdate = false;
            }
        }

        private void OnEnable()
        {
            _eventManager.OnSetFSMRunning += HandleSetFSMRunning;
        }

        private void OnDisable()
        {
            _eventManager.OnSetFSMRunning -= HandleSetFSMRunning;
        }

        private void Update()
        {
            if(_firstFrame)
            {
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

        private void HandleSetFSMRunning(Type fsmType, bool running)
        {
            var target = _fsms.FirstOrDefault(f => f.GetType() == fsmType);

            if (target != null)
            {
                target.Running = running;
            }
        }
    }
}