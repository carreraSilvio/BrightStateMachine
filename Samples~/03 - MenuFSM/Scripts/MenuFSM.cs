using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class MenuFSM : PushdownFSM
    {
        private void Awake()
        {
            var optionsState = new OptionsState();

            var videoOptionsState = new VideoOptionsState();
            var audioOptionsState = new AudioOptionsState();
            var controlsOptionsState = new ControlsOptionsState();

            AddPushTransition(optionsState, videoOptionsState, () => { return Input.GetKeyDown(KeyCode.Space); });
            AddPushTransition(videoOptionsState, controlsOptionsState, () => { return Input.GetKeyDown(KeyCode.Alpha1); });
            AddPushTransition(videoOptionsState, audioOptionsState, () => { return Input.GetKeyDown(KeyCode.Alpha2); });
            
            AddPopTransition(videoOptionsState, () => { return Input.GetKeyDown(KeyCode.Escape); });
            AddPopTransition(controlsOptionsState, () => { return Input.GetKeyDown(KeyCode.Escape); });
            AddPopTransition(audioOptionsState, () => { return Input.GetKeyDown(KeyCode.Escape); });

            _initialState = optionsState;
            
            OnStateEnter += HandleStateEnter;
            OnStateExit += HandleStatExit;
            OnStateResume += HandleStateResume;
            OnStateSuspend += HandleStateSuspend;


            DisplayName = "MenuFSM";
            LogTransitions = true;
        }

        private void Start()
        {

            ChangeToInitialState();
        }

        private void HandleStateSuspend(State state)
        {
        }

        private void HandleStateResume(State state)
        {
        }

        private void HandleStatExit(State state)
        {
        }

        private void HandleStateEnter(State state)
        {
        }

    }


}