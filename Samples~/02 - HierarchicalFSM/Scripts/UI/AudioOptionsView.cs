using BrightLib.StateMachine.Runtime;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class AudioOptionsView : MonoBehaviour
    {
        [SerializeField]
        private PushdownFSM _fsm;

        private void Awake()
        {
            _fsm.OnStateEnter += HandleStateEnter;
            _fsm.OnStateExit += HandleStateExit;
            gameObject.SetActive(false);
        }

        private void HandleStateEnter(State state)
        {
            if (state is AudioOptionsState)
            {
                gameObject.SetActive(true);
            }
        }

        private void HandleStateExit(State state)
        {
            if (state is AudioOptionsState)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
