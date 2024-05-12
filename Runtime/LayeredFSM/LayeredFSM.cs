using UnityEngine;

namespace BrightLib.StateMachine.Runtime
{
    /// <summary>
    /// Manages multiple FSMs
    /// </summary>
    public sealed class LayeredFSM : MonoBehaviour
    {
        [SerializeField]
        public FSM[] _fsms;


    }
}