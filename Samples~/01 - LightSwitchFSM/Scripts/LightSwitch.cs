using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    [RequireComponent(typeof(FSM))]
    public sealed class LightSwitch : MonoBehaviour
    {
        [SerializeField] 
        private FSM _fsm;
        [SerializeField] 
        private Light _light;

        private void Reset()
        {
            _fsm = GetComponent<FSM>();
        }

        private void Awake()
        {
            var offState = new OffState();
            var onState = new OnState();
            _fsm.AddTransition(offState, onState, () => 
            { 
                return Input.GetKeyDown(KeyCode.Space); 
            });
            _fsm.AddTransition(onState, offState, () => 
            { 
                return Input.GetKeyDown(KeyCode.Space); 
            });

            _fsm.SetInitialState(offState);
            _fsm.OnStateEnter += HandleStateEnter;
            _fsm.ChangeToInitialState();

            _light.enabled = false;
        }

        private void HandleStateEnter(State state)
        {
            _light.enabled = !_light.enabled;
        }

    }
}