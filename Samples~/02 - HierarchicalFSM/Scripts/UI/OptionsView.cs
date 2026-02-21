using BrightLib.StateMachine.Runtime;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BrightLib.StateMachine.Samples
{
    public class OptionsView : MonoBehaviour
    {
        [SerializeField]
        private PushdownFSM _fsm;

        [SerializeField]
        private Button _videoOptionsBtn;
        [SerializeField]
        private Button _audioOptionsBtn;
        [SerializeField]
        private Button _controlsOptionsBtn;

        private void Awake()
        {
            _fsm.OnStateEnter += HandleStateEnter;
            _fsm.OnStateExit += HandleStateExit;
            _fsm.OnStateResume += HandleStateResume;
            _fsm.OnStateSuspend += HandleStateSuspend;
            _videoOptionsBtn.onClick.AddListener(() => { _fsm.RequestPushState<VideoOptionsState>(); });
            _audioOptionsBtn.onClick.AddListener(() => { _fsm.RequestPushState<AudioOptionsState>(); });
            _controlsOptionsBtn.onClick.AddListener(() => { _fsm.RequestPushState<ControlsOptionsState>(); });
            gameObject.SetActive(false);
        }

        private void HandleStateEnter(State state)
        {
            if (state is OptionsState)
            {
                gameObject.SetActive(true);
            }
        }

        private void HandleStateExit(State state)
        {
            if (state is OptionsState)
            {
                gameObject.SetActive(false);
            }
        }

        private void HandleStateResume(State state)
        {
            if (state is OptionsState)
            {
                gameObject.SetActive(true);
            }
        }

        private void HandleStateSuspend(State state)
        {
            if (state is OptionsState)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
