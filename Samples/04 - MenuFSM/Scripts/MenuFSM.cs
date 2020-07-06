using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    /// <summary>
    /// The ovenHFSM will have two states, one of the states will have two child states
    /// State Off (entry)
    /// State On 
    ///    - Child State Heating Up(entry)
    ///    - Child State Heated Up
    /// Every time you transition to OnState you'll actually enter the HeatingUpState
    /// </summary>
    public class MenuFSM : PushdownFSM
    {

        private void Start()
        {
            var homeState = new HomeState();

            var optionsState = new OptionsState();
            var audioOptionsState = new AudioOptionsState();
            var controlsState = new ControlsState();

            AddOverlapTransition(homeState, optionsState, () => { return Input.GetKeyDown(KeyCode.Space); });

            AddOverlapTransition(optionsState, controlsState, () => { return Input.GetKeyDown(KeyCode.Alpha1); });
            AddOverlapTransition(optionsState, audioOptionsState, () => { return Input.GetKeyDown(KeyCode.Alpha2); });
            AddReturnTransition(optionsState, () => { return Input.GetKeyDown(KeyCode.Escape); });

            AddReturnTransition(controlsState, () => { return Input.GetKeyDown(KeyCode.Escape); });
            AddReturnTransition(audioOptionsState, () => { return Input.GetKeyDown(KeyCode.Escape); });

            _initialState = homeState;

        
            
            OnStateExit += HandleStatExit;
            OnStateEnter += HandleStateEnter;
            OnStateFocus += HandleStateFocus;
            OnStateObscure += HandleStateObscure;

            ChangeToInitialState();
        }

        private void HandleStateObscure(State state)
        {
            Debug.Log($"Obscure State \t{state.FullName()}");
        }

        private void HandleStateFocus(State state)
        {
            Debug.Log($"Focus State \t{state.FullName()}");
        }

        private void HandleStatExit(State state)
        {
            Debug.Log($"Exit State \t{state.FullName()}");
        }

        private void HandleStateEnter(State state)
        {
            Debug.Log($"Enter State \t{state.FullName()}");
        }

    }


}